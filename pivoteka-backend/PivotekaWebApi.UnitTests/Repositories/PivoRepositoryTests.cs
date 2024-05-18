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
    public class PivoRepositoryTests
    {
        private async Task<PivotekaContext> GetDatabaseContext()
        {
            var option = new DbContextOptionsBuilder<PivotekaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var dbContext = new PivotekaContext(option);
            dbContext.Database.EnsureCreated();
            if(await dbContext.Pivo.CountAsync() <= 0)
            {
                for(int i = 0; i < 10; i++)
                {
                    dbContext.Pivo.Add(new DbModels.Pivo
                    {
                        Ime = $"Pivo{i}",
                        Cijena = 10,
                        Kolicina = 10,
                        Opis = "Opis",
                        ZemljaPodrijetla = "Hrvatska",
                        NetoVolumen = 500,
                        ImeDobavljaca = "Samo dobra piva",
                        Vrsta = "Lager"
                    });
                    await dbContext.SaveChangesAsync();
                }
            }
            return dbContext;
        }

        [Fact]
        public async void PivoRepository_Exists_ReturnsTrue()
        {
            // Arrange
            var ime = "Pivo1";
            var dbContext = await GetDatabaseContext();
            var repository = new PivoRepository(dbContext);
            // Act
            var result = repository.Exists(ime);
            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void PivoRepository_Exists_WithModel_ReturnsTrue()
        {
            // Arrange
            var pivo = new DbModels.Pivo
            {
                Ime = "Pivo1",
                Cijena = 10,
                Kolicina = 10,
                Opis = "Opis",
                ZemljaPodrijetla = "Hrvatska",
                NetoVolumen = 500,
                ImeDobavljaca = "Samo dobra piva",
                Vrsta = "Lager"
            };
            var dbContext = await GetDatabaseContext();
            var repository = new PivoRepository(dbContext);
            // Act
            var result = repository.Exists(pivo);
            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async void PivoRepository_Get_ReturnsPivo()
        {
            // Arrange
            var ime = "Pivo1";
            var dbContext = await GetDatabaseContext();
            var repository = new PivoRepository(dbContext);

            // Act
            var result = repository.Get(ime);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Option<DbModels.Pivo>>();
        }


        [Fact]
        public async void PivoRepository_GetAll_ReturnsAllPivo()
        {
            // Arrange
            var dbContext = await GetDatabaseContext();
            var repository = new PivoRepository(dbContext);
            // Act
            var result = repository.GetAll();
            // Assert
            result.Should().NotBeNull();
            result.Count().Should().Be(10);
        }

        [Fact]
        public async void PivoRepository_Insert_ReturnsTrue()
        {
            // Arrange
            var pivo = new DbModels.Pivo
            {
                Ime = "Pivo11",
                Cijena = 10,
                Kolicina = 10,
                Opis = "Opis",
                ZemljaPodrijetla = "Hrvatska",
                NetoVolumen = 500,
                ImeDobavljaca = "Samo dobra piva",
                Vrsta = "Lager"
            };
            var dbContext = await GetDatabaseContext();
            var repository = new PivoRepository(dbContext);
            // Act
            var result = repository.Insert(pivo);
            // Assert
            result.Should().BeTrue();
        }

    }
}
