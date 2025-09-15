using Academia.Domain.Entities;
using Academia.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PresencasController : ControllerBase
    {
        private readonly IPresencaService _presencaService;
        public PresencasController(IPresencaService presencaService)
        {
            _presencaService = presencaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPresencas()
        {
            var presencas = await _presencaService.GetPresencasAsync();
            return Ok(presencas);
        }

        [HttpPost]
        public async Task<IActionResult> PostPresenca([FromBody] Presenca presenca)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (success, error, createdPresenca) = await _presencaService.CreatePresencaAsync(presenca);
            if (!success)
                return BadRequest(error);

            return CreatedAtAction(nameof(GetPresencas), new { id = createdPresenca!.Id }, createdPresenca);
        }
    }
}
