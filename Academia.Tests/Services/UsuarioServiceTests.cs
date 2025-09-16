using Academia.Api.Services;
using Academia.Domain.Entities;
using Academia.Infrastructure.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Academia.Tests.Services
{
    public class UsuarioServiceTests
    {
        private UsuarioService GetService(DbContextOptions<AcademiaDbContext> options)
        {
            var context = new AcademiaDbContext(options);
            return new UsuarioService(context);
        }

        [Fact]
        public async Task CreateUsuarioAsync_ShouldCreateUser_WhenEmailIsUnique()
        {
            var options = new DbContextOptionsBuilder<AcademiaDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateUser_Success").Options;
            var service = GetService(options);

            var result = await service.CreateUsuarioAsync("Teste", "teste@email.com", "123", "Admin", new List<int>());

            result.Success.Should().BeTrue();
            result.Usuario.Should().NotBeNull();
            result.Usuario.Email.Should().Be("teste@email.com");
        }

        [Fact]
        public async Task CreateUsuarioAsync_ShouldReturnError_WhenEmailExists()
        {
            var options = new DbContextOptionsBuilder<AcademiaDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateUser_Duplicate").Options;
            var service = GetService(options);
            await service.CreateUsuarioAsync("Teste", "teste@email.com", "123", "Admin", new List<int>());

            var result = await service.CreateUsuarioAsync("Outro", "teste@email.com", "456", "Admin", new List<int>());

            result.Success.Should().BeFalse();
            result.Error.Should().Be("E-mail já cadastrado.");
        }

        [Fact]
        public void HashPassword_ShouldReturnHash()
        {
            var hash = UsuarioService.HashPassword("senha123");
            hash.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task CreateUsuarioAsync_ShouldCreateUser_WithPermissoes()
        {
            var options = new DbContextOptionsBuilder<AcademiaDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateUser_WithPermissoes").Options;
            var service = GetService(options);
            var permissoes = new List<int> { 1, 2 };
            var result = await service.CreateUsuarioAsync("Teste", "teste2@email.com", "123", "Admin", permissoes);
            result.Success.Should().BeTrue();
            result.Usuario.Should().NotBeNull();
            result.Usuario.Email.Should().Be("teste2@email.com");
        }

        [Fact]
        public async Task CreateUsuarioAsync_ShouldCreateUser_WithoutPermissoes()
        {
            var options = new DbContextOptionsBuilder<AcademiaDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateUser_WithoutPermissoes").Options;
            var service = GetService(options);
            var result = await service.CreateUsuarioAsync("Teste", "teste3@email.com", "123", "Admin", new List<int>());
            result.Success.Should().BeTrue();
            result.Usuario.Should().NotBeNull();
            result.Usuario.Email.Should().Be("teste3@email.com");
        }
    }
}
