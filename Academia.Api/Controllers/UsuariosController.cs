using Academia.Api.Services;
using Academia.Api.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _usuarioService.GetUsuariosAsync();
            return Ok(usuarios);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostUsuario([FromBody] UsuarioRequest usuarioRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (success, error, usuario) = await _usuarioService.CreateUsuarioAsync(
                usuarioRequest.Nome,
                usuarioRequest.Email,
                usuarioRequest.Password,
                usuarioRequest.Perfil,
                usuarioRequest.PermissoesIds
            );
            if (!success)
                return Conflict(error);

            return CreatedAtAction(nameof(GetUsuarios), new { id = usuario!.Id }, usuario);
        }
    }
}
