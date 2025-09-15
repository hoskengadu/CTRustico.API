using Academia.Api.Services;
using Academia.Domain.Entities;
using Academia.Infrastructure.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace Academia.Tests.Services
{
    public class PresencaServiceTests
    {
        private PresencaService GetService(DbContextOptions<AcademiaDbContext> options)
        {
            var context = new AcademiaDbContext(options);
            return new PresencaService(context);
        }

        [Fact]
        public async Task CreatePresencaAsync_ShouldCreatePresenca_WhenValid()
        {
            var options = new DbContextOptionsBuilder<AcademiaDbContext>()
                .UseInMemoryDatabase(databaseName: "CreatePresenca_Success").Options;
            var service = GetService(options);
            var presenca = new Presenca { AlunoId = 1 };

            var (success, error, createdPresenca) = await service.CreatePresencaAsync(presenca);

            success.Should().BeTrue();
            createdPresenca.Should().NotBeNull();
            createdPresenca.AlunoId.Should().Be(1);
        }
    }
}
