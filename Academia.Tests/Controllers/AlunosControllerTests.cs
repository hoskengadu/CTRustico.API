using Academia.Api.Controllers;
using Academia.Api.Services;
using Academia.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Academia.Tests.Controllers
{
    public class AlunosControllerTests
    {
        [Fact]
        public async Task GetAlunos_ShouldReturnOkWithList()
        {
            var mockService = new Mock<IAlunoService>();
            mockService.Setup(s => s.GetAlunosAsync()).ReturnsAsync(new List<Aluno> { new Aluno { Id = 1, Nome = "A" } });
            var controller = new AlunosController(mockService.Object);
            var result = await controller.GetAlunos();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task PostAluno_ShouldReturnBadRequest_WhenModelIsInvalid()
        {
            var mockService = new Mock<IAlunoService>();
            var controller = new AlunosController(mockService.Object);
            controller.ModelState.AddModelError("Nome", "Required");
            var result = await controller.PostAluno(null);
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task PostAluno_ShouldReturnBadRequest_WhenServiceReturnsError()
        {
            var mockService = new Mock<IAlunoService>();
            mockService.Setup(s => s.CreateAlunoAsync(It.IsAny<Aluno>()))
                .ReturnsAsync((false, "Erro ao criar aluno", null));
            var controller = new AlunosController(mockService.Object);
            var aluno = new Aluno { Nome = "Teste" };
            var result = await controller.PostAluno(aluno);
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
