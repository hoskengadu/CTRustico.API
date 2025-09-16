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
    public class PresencasControllerTests
    {
        [Fact]
        public async Task GetPresencas_ShouldReturnOkWithList()
        {
            var mockService = new Mock<IPresencaService>();
            mockService.Setup(s => s.GetPresencasAsync()).ReturnsAsync(new List<Presenca> { new Presenca { Id = 1, AlunoId = 1 } });
            var controller = new PresencasController(mockService.Object);
            var result = await controller.GetPresencas();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task PostPresenca_ShouldReturnBadRequest_WhenModelIsInvalid()
        {
            var mockService = new Mock<IPresencaService>();
            var controller = new PresencasController(mockService.Object);
            controller.ModelState.AddModelError("AlunoId", "Required");
            var result = await controller.PostPresenca(null);
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task PostPresenca_ShouldReturnBadRequest_WhenServiceReturnsError()
        {
            var mockService = new Mock<IPresencaService>();
            mockService.Setup(s => s.CreatePresencaAsync(It.IsAny<Presenca>()))
                .ReturnsAsync((false, "Erro ao criar presenca", null));
            var controller = new PresencasController(mockService.Object);
            var presenca = new Presenca { AlunoId = 1 };
            var result = await controller.PostPresenca(presenca);
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
