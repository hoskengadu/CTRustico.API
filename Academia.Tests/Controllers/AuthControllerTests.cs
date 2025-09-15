using Academia.Api.Controllers;
using Academia.Api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Academia.Tests.Controllers
{
    public class AuthControllerTests
    {
        [Fact]
        public async Task Login_ShouldReturnUnauthorized_WhenUserNull()
        {
            var mockAuth = new Mock<IAuthService>();
            var mockUsuario = new Mock<IUsuarioService>();
            mockAuth.Setup(a => a.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Academia.Domain.Entities.Usuario)null);
            var controller = new AuthController(mockAuth.Object, mockUsuario.Object);
            var loginRequest = new Academia.Api.DTOs.LoginRequest { Email = "x", Password = "y" };
            var result = await controller.Login(loginRequest);
            result.Should().BeOfType<UnauthorizedObjectResult>();
        }
    }
}
