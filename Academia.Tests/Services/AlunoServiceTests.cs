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

        [Fact]
        public async Task CreateAlunoAsync_ShouldReturnError_WhenAlunoIsNull()
        {
            var options = new DbContextOptionsBuilder<AcademiaDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateAluno_Null").Options;
            var service = GetService(options);

            var (success, error, createdAluno) = await service.CreateAlunoAsync(null);

            success.Should().BeFalse();
            error.Should().Be("Aluno inválido.");
            createdAluno.Should().BeNull();
        }

        [Fact]
        public async Task GetAlunosAsync_ShouldReturnEmptyList_WhenNoAlunos()
        {
            var options = new DbContextOptionsBuilder<AcademiaDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAlunos_Empty").Options;
            var service = GetService(options);

            var alunos = await service.GetAlunosAsync();

            alunos.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAlunosAsync_ShouldReturnList_WhenAlunosExist()
        {
            var options = new DbContextOptionsBuilder<AcademiaDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAlunos_WithData").Options;

            using (var context = new AcademiaDbContext(options))
            {
                var plano = new Plano { Nome = "Plano Teste", Valor = 100 };
                context.Planos.Add(plano);
                context.SaveChanges();

                context.Alunos.Add(new Aluno { Nome = "Aluno 1", CPF = "123", PlanoId = plano.Id });
                context.SaveChanges();

                var service = new AlunoService(context);
                var alunos = await service.GetAlunosAsync();
                alunos.Should().NotBeEmpty();
                alunos[0].Nome.Should().Be("Aluno 1");
            }
        }
    }
}
