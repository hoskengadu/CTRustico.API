using Academia.Domain.Entities;
using Academia.Api.Services;
using Academia.Api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUsuarioService _usuarioService;

        public AuthController(IAuthService authService, IUsuarioService usuarioService)
        {
            _authService = authService;
            _usuarioService = usuarioService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _authService.AuthenticateAsync(request.Email, request.Password);
            if (user == null)
                return Unauthorized("Usuário ou senha inválidos");

            var token = _authService.GenerateJwtToken(user);
            return Ok(new { token });
        }
    }

    // DTO movido para Academia.Api.DTOs.LoginRequest
}
