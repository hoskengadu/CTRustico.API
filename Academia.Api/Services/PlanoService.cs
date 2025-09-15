using Academia.Domain.Entities;
using Academia.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Academia.Api.Services
{
    public class PlanoService : IPlanoService
    {
        private readonly AcademiaDbContext _context;
        public PlanoService(AcademiaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Plano>> GetPlanosAsync()
        {
            return await _context.Planos.ToListAsync();
        }

        public async Task<(bool Success, string? Error, Plano? Plano)> CreatePlanoAsync(Plano plano)
        {
            if (plano == null)
                return (false, "Plano inválido.", null);
            _context.Planos.Add(plano);
            await _context.SaveChangesAsync();
            return (true, null, plano);
        }
    }
}
