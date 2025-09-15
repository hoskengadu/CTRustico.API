using Academia.Domain.Entities;

namespace Academia.Api.Services
{
    public interface IPresencaService
    {
        Task<List<Presenca>> GetPresencasAsync();
        Task<(bool Success, string? Error, Presenca? Presenca)> CreatePresencaAsync(Presenca presenca);
    }
}
