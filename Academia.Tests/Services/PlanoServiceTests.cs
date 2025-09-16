using Academia.Api.Services;
using Academia.Domain.Entities;
using Academia.Infrastructure.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace Academia.Tests.Services
{
    public class PlanoServiceTests
    {
        private PlanoService GetService(DbContextOptions<AcademiaDbContext> options)
        {
            var context = new AcademiaDbContext(options);
            return new PlanoService(context);
        }

        [Fact]
        public async Task CreatePlanoAsync_ShouldCreatePlano_WhenValid()
        {
            var options = new DbContextOptionsBuilder<AcademiaDbContext>()
                .UseInMemoryDatabase(databaseName: "CreatePlano_Success").Options;
            var service = GetService(options);
            var plano = new Plano { Nome = "Plano Teste", Valor = 100 };

            var (success, error, createdPlano) = await service.CreatePlanoAsync(plano);

            success.Should().BeTrue();
            createdPlano.Should().NotBeNull();
            createdPlano.Nome.Should().Be("Plano Teste");
        }

        [Fact]
        public async Task CreatePlanoAsync_ShouldReturnError_WhenPlanoIsNull()
        {
            var options = new DbContextOptionsBuilder<AcademiaDbContext>()
                .UseInMemoryDatabase(databaseName: "CreatePlano_Null").Options;
            var service = GetService(options);

            var (success, error, createdPlano) = await service.CreatePlanoAsync(null);

            success.Should().BeFalse();
            error.Should().Be("Plano inválido.");
            createdPlano.Should().BeNull();
        }

        [Fact]
        public async Task GetPlanosAsync_ShouldReturnEmptyList_WhenNoPlanos()
        {
            var options = new DbContextOptionsBuilder<AcademiaDbContext>()
                .UseInMemoryDatabase(databaseName: "GetPlanos_Empty").Options;
            var service = GetService(options);
            var planos = await service.GetPlanosAsync();
            planos.Should().BeEmpty();
        }

        [Fact]
        public async Task GetPlanosAsync_ShouldReturnList_WhenPlanosExist()
        {
            var options = new DbContextOptionsBuilder<AcademiaDbContext>()
                .UseInMemoryDatabase(databaseName: "GetPlanos_WithData").Options;
            using (var context = new AcademiaDbContext(options))
            {
                context.Planos.Add(new Plano { Nome = "Plano 1", Valor = 50 });
                context.SaveChanges();
            }
            var service = GetService(options);
            var planos = await service.GetPlanosAsync();
            planos.Should().NotBeEmpty();
            planos[0].Nome.Should().Be("Plano 1");
        }

        [Fact]
        public void Plano_Properties_ShouldSetAndGetValues()
        {
            var plano = new Plano
            {
                Id = 1,
                Nome = "Plano Teste",
                Valor = 99.99M
            };
            Assert.Equal(1, plano.Id);
            Assert.Equal("Plano Teste", plano.Nome);
            Assert.Equal(99.99M, plano.Valor);
            Assert.NotNull(plano.Alunos);
        }
    }
}
