using Academia.Domain.Entities;

namespace Academia.Api.Services
{
    public interface IPlanoService
    {
        Task<List<Plano>> GetPlanosAsync();
        Task<(bool Success, string? Error, Plano? Plano)> CreatePlanoAsync(Plano plano);
    }
}
