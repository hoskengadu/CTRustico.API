using System.Collections.Generic;
namespace Academia.Domain.Entities
{
    public class Permissao
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public ICollection<UsuarioPermissao> UsuariosPermissoes { get; set; } = new List<UsuarioPermissao>();
    }
}
