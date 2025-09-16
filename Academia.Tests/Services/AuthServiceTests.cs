using Academia.Api.Services;
using Academia.Domain.Entities;
using Academia.Infrastructure.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Academia.Tests.Services
{
    public class AuthServiceTests
    {
        private AuthService GetService(DbContextOptions<AcademiaDbContext> options, IConfiguration config)
        {
            var context = new AcademiaDbContext(options);
            return new AuthService(context, config);
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldReturnNull_WhenUserNotFound()
        {
            var options = new DbContextOptionsBuilder<AcademiaDbContext>()
                .UseInMemoryDatabase(databaseName: "Auth_UserNotFound").Options;
            var config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string>()).Build();
            var service = GetService(options, config);

            var result = await service.AuthenticateAsync("naoexiste@email.com", "senha");
            result.Should().BeNull();
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldReturnUser_WhenCredentialsAreValid()
        {
            var options = new DbContextOptionsBuilder<AcademiaDbContext>()
                .UseInMemoryDatabase(databaseName: "Auth_ValidUser").Options;
            var config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string> {
                {"Jwt:Key", "test-key-12345678-abcdef-9876543210abcd"}, // 32 chars (256 bits)
                {"Jwt:Issuer", "test-issuer"},
                {"Jwt:Audience", "test-audience"},
                {"Jwt:ExpireHours", "1"}
            }).Build();
            using (var context = new AcademiaDbContext(options))
            {
                context.Usuarios.Add(new Usuario { Email = "user@email.com", SenhaHash = UsuarioService.HashPassword("123") });
                context.SaveChanges();
                var service = new AuthService(context, config);
                var result = await service.AuthenticateAsync("user@email.com", "123");
                result.Should().NotBeNull();
                result.Email.Should().Be("user@email.com");
            }
        }

        [Fact]
        public void GenerateJwtToken_ShouldReturnToken()
        {
            var config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string> {
                {"Jwt:Key", "test-key-12345678-abcdef-9876543210abcd"}, // 32 chars (256 bits)
                {"Jwt:Issuer", "test-issuer"},
                {"Jwt:Audience", "test-audience"},
                {"Jwt:ExpireHours", "1"}
            }).Build();
            var usuario = new Usuario { Id = 1, Email = "user@email.com", Perfil = "Admin" };
            var service = new AuthService(new AcademiaDbContext(new DbContextOptionsBuilder<AcademiaDbContext>().Options), config);
            var token = service.GenerateJwtToken(usuario);
            token.Should().NotBeNullOrEmpty();
        }
    }
}
