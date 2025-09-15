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
    public class UsuariosControllerTests
    {
        [Fact]
        public async Task PostUsuario_ShouldReturnCreated_WhenSuccess()
        {
            var mockService = new Mock<IUsuarioService>();
            mockService.Setup(s => s.CreateUsuarioAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<List<int>>()))
                .ReturnsAsync((true, null, new Usuario { Id = 1, Nome = "Teste", Email = "teste@email.com" }));
            var controller = new UsuariosController(mockService.Object);
            var request = new Academia.Api.DTOs.UsuarioRequest
            {
                Nome = "Teste",
                Email = "teste@email.com",
                Password = "123",
                Perfil = "Admin",
                PermissoesIds = new List<int> { 1 }
            };

            var result = await controller.PostUsuario(request);
            result.Should().BeOfType<CreatedAtActionResult>();
        }
    }
}
