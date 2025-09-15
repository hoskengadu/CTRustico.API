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
    }
}
