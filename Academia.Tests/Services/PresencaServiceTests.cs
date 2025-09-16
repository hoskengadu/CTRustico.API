using Academia.Api.Services;
using Academia.Domain.Entities;
using Academia.Infrastructure.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
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

        [Fact]
        public void Presenca_Properties_ShouldSetAndGetValues()
        {
            var presenca = new Presenca
            {
                Id = 1,
                AlunoId = 2,
                DataPresenca = new DateTime(2023, 1, 1)
            };
            Assert.Equal(1, presenca.Id);
            Assert.Equal(2, presenca.AlunoId);
            Assert.Equal(new DateTime(2023, 1, 1), presenca.DataPresenca);
        }

        [Fact]
        public async Task GetPresencasAsync_ShouldReturnEmptyList_WhenNoPresencas()
        {
            var options = new DbContextOptionsBuilder<AcademiaDbContext>()
                .UseInMemoryDatabase(databaseName: "GetPresencas_Empty").Options;
            var service = new PresencaService(new AcademiaDbContext(options));
            var presencas = await service.GetPresencasAsync();
            presencas.Should().BeEmpty();
        }

        [Fact]
        public async Task GetPresencasAsync_ShouldReturnList_WhenPresencasExist()
        {
            var options = new DbContextOptionsBuilder<AcademiaDbContext>()
                .UseInMemoryDatabase(databaseName: "GetPresencas_WithData").Options;
            using (var context = new AcademiaDbContext(options))
            {
                context.Presencas.Add(new Presenca { AlunoId = 1, DataPresenca = DateTime.Now });
                context.SaveChanges();
                var service = new PresencaService(context);
                var presencas = await service.GetPresencasAsync();
                presencas.Should().NotBeEmpty();
            }
        }

        [Fact]
        public async Task CreatePresencaAsync_ShouldReturnError_WhenPresencaIsNull()
        {
            var options = new DbContextOptionsBuilder<AcademiaDbContext>()
                .UseInMemoryDatabase(databaseName: "CreatePresenca_Null").Options;
            var service = new PresencaService(new AcademiaDbContext(options));
            var (success, error, presenca) = await service.CreatePresencaAsync(null);
            success.Should().BeFalse();
            error.Should().Be("Presença inválida.");
            presenca.Should().BeNull();
        }
    }
}
