using Academia.Domain.Entities;
using Academia.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Academia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AlunosController : ControllerBase
        {
            private readonly IAlunoService _alunoService;
            public AlunosController(IAlunoService alunoService)
            {
                _alunoService = alunoService;
            }

        [HttpGet]
        public async Task<IActionResult> GetAlunos()
        {
            var alunos = await _alunoService.GetAlunosAsync();
            return Ok(alunos);
        }

        [HttpPost]
        public async Task<IActionResult> PostAluno([FromBody] Aluno aluno)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var (success, error, createdAluno) = await _alunoService.CreateAlunoAsync(aluno);
            if (!success)
                return BadRequest(error);

            return CreatedAtAction(nameof(GetAlunos), new { id = createdAluno!.Id }, createdAluno);
        }
    }
}
