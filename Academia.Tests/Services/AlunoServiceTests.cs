using Academia.Api.Services;
using Academia.Domain.Entities;
using Academia.Infrastructure.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Academia.Tests.Services
{
    public class AlunoServiceTests
    {
        private AlunoService GetService(DbContextOptions<AcademiaDbContext> options)
        {
            var context = new AcademiaDbContext(options);
            return new AlunoService(context);
        }

        [Fact]
        public async Task CreateAlunoAsync_ShouldCreateAluno_WhenValid()
        {
            var options = new DbContextOptionsBuilder<AcademiaDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateAluno_Success").Options;
            var service = GetService(options);
            var aluno = new Aluno { Nome = "Aluno Teste", CPF = "12345678900" };

            var (success, error, createdAluno) = await service.CreateAlunoAsync(aluno);

            success.Should().BeTrue();
            createdAluno.Should().NotBeNull();
            createdAluno.Nome.Should().Be("Aluno Teste");
        }
    }
}
