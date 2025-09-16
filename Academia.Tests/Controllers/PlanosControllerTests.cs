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
    public class PlanosControllerTests
    {
        [Fact]
        public async Task GetPlanos_ShouldReturnOkWithList()
        {
            var mockService = new Mock<IPlanoService>();
            mockService.Setup(s => s.GetPlanosAsync()).ReturnsAsync(new List<Plano> { new Plano { Id = 1, Nome = "P" } });
            var controller = new PlanosController(mockService.Object);
            var result = await controller.GetPlanos();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task PostPlano_ShouldReturnBadRequest_WhenModelIsInvalid()
        {
            var mockService = new Mock<IPlanoService>();
            var controller = new PlanosController(mockService.Object);
            controller.ModelState.AddModelError("Nome", "Required");
            var result = await controller.PostPlano(null);
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task PostPlano_ShouldReturnBadRequest_WhenServiceReturnsError()
        {
            var mockService = new Mock<IPlanoService>();
            mockService.Setup(s => s.CreatePlanoAsync(It.IsAny<Plano>()))
                .ReturnsAsync((false, "Erro ao criar plano", null));
            var controller = new PlanosController(mockService.Object);
            var plano = new Plano { Nome = "Plano Teste" };
            var result = await controller.PostPlano(plano);
            result.Should().BeOfType<BadRequestObjectResult>();
        }
    }
}
