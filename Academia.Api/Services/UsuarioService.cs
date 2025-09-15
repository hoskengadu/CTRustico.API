using Academia.Domain.Entities;
using Academia.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Academia.Api.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AcademiaDbContext _context;
        public UsuarioService(AcademiaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> GetUsuariosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<(bool Success, string? Error, Usuario? Usuario)> CreateUsuarioAsync(string nome, string email, string password, string perfil, List<int> permissoesIds)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == email))
                return (false, "E-mail já cadastrado.", null);

            var usuario = new Usuario
            {
                Nome = nome,
                Email = email,
                Perfil = perfil,
                SenhaHash = HashPassword(password)
            };
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // Relaciona permissões
            if (permissoesIds != null && permissoesIds.Count > 0)
            {
                foreach (var permissaoId in permissoesIds)
                {
                    _context.UsuariosPermissoes.Add(new UsuarioPermissao
                    {
                        UsuarioId = usuario.Id,
                        PermissaoId = permissaoId
                    });
                }
                await _context.SaveChangesAsync();
            }

            return (true, null, usuario);
        }

        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
