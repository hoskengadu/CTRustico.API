using Academia.Domain.Entities;

namespace Academia.Api.Services
{
    public interface IAuthService
    {
        Task<Usuario?> AuthenticateAsync(string email, string password);
        string GenerateJwtToken(Usuario user);
    }
}
