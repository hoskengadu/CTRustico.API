using Academia.Domain.Entities;
using Academia.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Academia.Api.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly AcademiaDbContext _context;
        public AlunoService(AcademiaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Aluno>> GetAlunosAsync()
        {
            return await _context.Alunos.Include(a => a.Plano).ToListAsync();
        }

        public async Task<(bool Success, string? Error, Aluno? Aluno)> CreateAlunoAsync(Aluno aluno)
        {
            if (aluno == null)
                return (false, "Aluno inválido.", null);
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();
            return (true, null, aluno);
        }
    }
}
