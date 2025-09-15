using Academia.Domain.Entities;
using Academia.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Academia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PlanosController : ControllerBase
    {
        private readonly IPlanoService _planoService;
        public PlanosController(IPlanoService planoService)
        {
            _planoService = planoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlanos()
        {
            var planos = await _planoService.GetPlanosAsync();
            return Ok(planos);
        }

        [HttpPost]
        public async Task<IActionResult> PostPlano([FromBody] Plano plano)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (success, error, createdPlano) = await _planoService.CreatePlanoAsync(plano);
            if (!success)
                return BadRequest(error);

            return CreatedAtAction(nameof(GetPlanos), new { id = createdPlano!.Id }, createdPlano);
        }
    }
}
