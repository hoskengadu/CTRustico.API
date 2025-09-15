using Academia.Domain.Entities;
using Academia.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Academia.Api.Services
{
    public class PresencaService : IPresencaService
    {
        private readonly AcademiaDbContext _context;
        public PresencaService(AcademiaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Presenca>> GetPresencasAsync()
        {
            return await _context.Presencas.Include(p => p.Aluno).ToListAsync();
        }

        public async Task<(bool Success, string? Error, Presenca? Presenca)> CreatePresencaAsync(Presenca presenca)
        {
            if (presenca == null)
                return (false, "Presença inválida.", null);
            _context.Presencas.Add(presenca);
            await _context.SaveChangesAsync();
            return (true, null, presenca);
        }
    }
}
