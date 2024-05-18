using Pivoteka.DataAccess.PostgreSQL.Data;
using Microsoft.EntityFrameworkCore;
using DbModels = Pivoteka.DataAccess.PostgreSQL.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pivoteka.Repositories.PostgreSQL;
using FluentAssertions;
using Pivoteka.Commons;

namespace PivotekaWebApi.UnitTests.Repositories
{
    public class VrstumRepositoryTests
    {
        private async Task<PivotekaContext> GetDatabaseContext()
        {
            var option = new DbContextOptionsBuilder<PivotekaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new PivotekaContext(option);
            dbContext.Database.EnsureCreated();
            if (await dbContext.Vrstum.CountAsync() <= 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    dbContext.Vrstum.Add(new DbModels.Vrstum
                    {
                        Ime = $"Vrstum{i}"
                    });
                }
                await dbContext.SaveChangesAsync();
            }
            return dbContext;
        }

        [Fact]
        public async void VrstumRepository_Exists_ReturnsTrue()
        {
            // Arrange
            var ime = "Vrstum1";
            var dbContext = await GetDatabaseContext();
            var repository = new VrstumRepository(dbContext);
            // Act
            var result = repository.Exists(ime);
            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void VrstumRepository_Exists_WithModel_ReturnsTrue()
        {
            // Arrange
            var ime = "Vrstum1";
            var dbContext = await GetDatabaseContext();
            var repository = new VrstumRepository(dbContext);
            // Act
            var result = repository.Exists(new DbModels.Vrstum { Ime = ime });
            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void VrstumRepository_Get_ReturnsVrstum()
        {
            // Arrange
            var ime = "Vrstum1";
            var dbContext = await GetDatabaseContext();
            var repository = new VrstumRepository(dbContext);
            // Act
            var result = repository.Get(ime);
            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async void VrstumRepository_GetAll_ReturnsVrstum()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var repository = new VrstumRepository(dbContext);
            // Act
            var result = repository.GetAll();
            // Assert
            result.Should().NotBeEmpty();
        }
    }
}
