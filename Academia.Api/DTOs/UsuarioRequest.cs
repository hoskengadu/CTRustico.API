using System.Collections.Generic;

namespace Academia.Api.DTOs
{
    public class UsuarioRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Perfil { get; set; } = "Recepcionista";
        public List<int> PermissoesIds { get; set; } = new List<int>();
    }
}
