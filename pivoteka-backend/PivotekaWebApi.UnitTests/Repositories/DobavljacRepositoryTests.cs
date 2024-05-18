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
    public class DobavljacRepositoryTests
    {
        private async Task<PivotekaContext> GetDatabaseContext()
        {
            var option = new DbContextOptionsBuilder<PivotekaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new PivotekaContext(option);
            dbContext.Database.EnsureCreated();
            if (await dbContext.Dobavljac.CountAsync() <= 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    dbContext.Dobavljac.Add(new DbModels.Dobavljac
                    {
                        Ime = $"Dobavljac{i}",
                        Adresa = $"Adresa{i}",
                        Email = $"Email{i}"
                    });
                }
                await dbContext.SaveChangesAsync();
            }
            return dbContext;
        }

        [Fact]
        public async void DobavljacRepository_Exists_ReturnsTrue()
        {
            // Arrange
            var ime = "Dobavljac1";
            var dbContext = await GetDatabaseContext();
            var repository = new DobavljacRepository(dbContext);
            // Act
            var result = repository.Exists(ime);
            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void DobavljacRepository_Exists_WithModel_ReturnsTrue()
        {
            // Arrange
            var ime = "Dobavljac1";
            var dbContext = await GetDatabaseContext();
            var repository = new DobavljacRepository(dbContext);
            // Act
            var result = repository.Exists(new DbModels.Dobavljac { Ime = ime });
            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void DobavljacRepository_Get_ReturnsDobavljac()
        {
            // Arrange
            var ime = "Dobavljac1";
            var dbContext = await GetDatabaseContext();
            var repository = new DobavljacRepository(dbContext);
            // Act
            var result = repository.Get(ime);
            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async void DobavljacRepository_GetAll_ReturnsDobavljac()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var repository = new DobavljacRepository(dbContext);
            // Act
            var result = repository.GetAll();
            // Assert
            result.Should().NotBeEmpty();
        }
    }
}
