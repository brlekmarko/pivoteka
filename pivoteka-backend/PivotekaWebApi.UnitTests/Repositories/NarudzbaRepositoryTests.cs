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
    public class NarudzbaRepositoryTests
    {
        private async Task<PivotekaContext> GetDatabaseContext()
        {
            var option = new DbContextOptionsBuilder<PivotekaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new PivotekaContext(option);
            dbContext.Database.EnsureCreated();
            if(await dbContext.Narudzba.CountAsync() <= 0)
            {
                for(int i = 0; i < 10; i++)
                {
                    dbContext.Narudzba.Add(new DbModels.Narudzba
                    {
                        Datum = DateTime.Now,
                        UkupnaCijena = 100,
                        KorisnickoIme = "Korisnik"
                    });
                }
                await dbContext.SaveChangesAsync();
            }
            return dbContext;
        }

        [Fact]
        public async void NarudzbaRepository_Exists_ReturnsTrue()
        {
            // Arrange
            var id = 1;
            var dbContext = await GetDatabaseContext();
            var repository = new NarudzbaRepository(dbContext);
            // Act
            var result = repository.Exists(id);
            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void NarudzbaRepository_Exists_WithModel_ReturnsTrue()
        {
            // Arrange
            var id = 1;
            var dbContext = await GetDatabaseContext();
            var repository = new NarudzbaRepository(dbContext);
            // Act
            var result = repository.Exists(new DbModels.Narudzba { Id = id });
            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void NarudzbaRepository_Get_ReturnsNarudzba()
        {
            // Arrange
            var id = 1;
            var dbContext = await GetDatabaseContext();
            var repository = new NarudzbaRepository(dbContext);
            // Act
            var result = repository.Get(id);
            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async void NarudzbaRepository_GetAll_ReturnsNarudzba()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var repository = new NarudzbaRepository(dbContext);
            // Act
            var result = repository.GetAll();
            // Assert
            result.Should().NotBeEmpty();
        }

        [Fact]
        public async void PivoRepository_Insert_ReturnsTrue()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var repository = new NarudzbaRepository(dbContext);
            var narudzba = new DbModels.Narudzba
            {
                Id = 11,
                Datum = DateTime.Now,
                UkupnaCijena = 100,
                KorisnickoIme = "Korisnik"
            };
            // Act
            var result = repository.Insert(narudzba);
            // Assert
            result.Should().BeTrue();
        }
    }
}
