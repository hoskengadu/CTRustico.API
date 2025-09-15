using Academia.Domain.Entities;

namespace Academia.Api.Services
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> GetUsuariosAsync();
    Task<(bool Success, string? Error, Usuario? Usuario)> CreateUsuarioAsync(string nome, string email, string password, string perfil, List<int> permissoesIds);
    }
}
