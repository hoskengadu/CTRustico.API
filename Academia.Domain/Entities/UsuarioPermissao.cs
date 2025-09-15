namespace Academia.Domain.Entities
{
    public class UsuarioPermissao
    {
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;
        public int PermissaoId { get; set; }
        public Permissao Permissao { get; set; } = null!;
    }
}
