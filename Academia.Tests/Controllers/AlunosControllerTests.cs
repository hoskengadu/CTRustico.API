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
    }
}
