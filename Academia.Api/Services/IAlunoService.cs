using Academia.Domain.Entities;

namespace Academia.Api.Services
{
    public interface IAlunoService
    {
        Task<List<Aluno>> GetAlunosAsync();
        Task<(bool Success, string? Error, Aluno? Aluno)> CreateAlunoAsync(Aluno aluno);
    }
}
