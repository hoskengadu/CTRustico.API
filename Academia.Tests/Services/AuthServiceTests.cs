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
    }
}
