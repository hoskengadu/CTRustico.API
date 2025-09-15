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
    }
}
