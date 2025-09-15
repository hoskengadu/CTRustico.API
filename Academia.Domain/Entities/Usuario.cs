using System.Collections.Generic;
namespace Academia.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SenhaHash { get; set; } = string.Empty;
        public string Perfil { get; set; } = string.Empty;

        public ICollection<UsuarioPermissao> UsuariosPermissoes { get; set; } = new List<UsuarioPermissao>();
    }
}