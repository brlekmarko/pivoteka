using Xunit;
using Moq;
using PivotekaWebApi.Controllers;
using DbModels = Pivoteka.DataAccess.PostgreSQL.Data.DbModels;
using Pivoteka.Repositories;
using Microsoft.AspNetCore.Mvc;
using PivotekaWebApi.DTOs;
using System.Collections.Generic;

namespace PivotekaWebApi.UnitTests
{
    public class PivoControllerTest
    {
        private readonly Mock<IPivoRepository<string, DbModels.Pivo>> _pivoRepositoryMock;
        private readonly PivoController _controller;

        public PivoControllerTest()
        {
            _pivoRepositoryMock = new Mock<IPivoRepository<string, DbModels.Pivo>>();
            _controller = new PivoController(_pivoRepositoryMock.Object);
        }

        [Fact]
        public void GetAllPivo_Success()
        {
            // Arrange
            var piva = new List<DbModels.Pivo>
            {
                new DbModels.Pivo { Ime = "Jelen" },
                new DbModels.Pivo { Ime = "Lav" }
            };
            _pivoRepositoryMock.Setup(repo => repo.GetAll()).Returns(piva);

            // Act
            var result = _controller.GetAllPivo();
            var resultType = result.Result as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(resultType);
            Assert.IsType<List<DbModels.Pivo>>(resultType.Value);
            Assert.Equal(2, ((List<DbModels.Pivo>)resultType.Value).Count);
        }

        [Fact]
        public void GetPivo_Success()
        {
            // Arrange
            var ime = "Jelen";
            var pivo = new DbModels.Pivo { Ime = ime };
            _pivoRepositoryMock.Setup(repo => repo.Get(ime)).Returns(pivo);

            // Act
            var result = _controller.GetPivo(ime);
            var resultType = result.Result as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(resultType);
            Assert.IsType<DbModels.Pivo>(resultType.Value);
            Assert.Equal(ime, ((DbModels.Pivo)resultType.Value).Ime);
        }

        [Fact]
        public void GetPivo_Fail()
        {
            // Arrange
            var ime = "Jelen2";
            _pivoRepositoryMock.Setup(repo => repo.Get(ime)).Returns((DbModels.Pivo)null);

            // Act
            var result = _controller.GetPivo(ime);
            var resultType = result.Result as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(resultType);
        }

        [Fact]
        public void EditPivo_Success()
        {
            // Arrange
            var ime = "Jelen";
            var pivo = new DbModels.Pivo
            {
                Ime = ime,
                Cijena = 100,
                Kolicina = 100,
                Opis = "Opis",
                ZemljaPodrijetla = "Srbija",
                Vrsta = "Lager"
            };
            _pivoRepositoryMock.Setup(repo => repo.Update(ime, It.IsAny<DbModels.Pivo>())).Returns(pivo);

            // Act
            var result = _controller.EditPivo(ime, pivo.ToDto());
            var resultType = result as AcceptedAtActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(resultType);
            Assert.IsType<AcceptedAtActionResult>(resultType);
            Assert.Equal(ime, ((DbModels.Pivo)resultType.Value).Ime);
            Assert.Equal(pivo.Cijena, ((DbModels.Pivo)resultType.Value).Cijena);
            Assert.Equal(pivo.Kolicina, ((DbModels.Pivo)resultType.Value).Kolicina);
            Assert.Equal(pivo.Opis, ((DbModels.Pivo)resultType.Value).Opis);
            Assert.Equal(pivo.ZemljaPodrijetla, ((DbModels.Pivo)resultType.Value).ZemljaPodrijetla);
            Assert.Equal(pivo.Vrsta, ((DbModels.Pivo)resultType.Value).Vrsta);
        }

        [Fact]
        public void EditPivo_Fail()
        {
            // Arrange
            var ime = "Jelen2";
            var pivo = new DbModels.Pivo
            {
                Ime = "Jelen",
                Cijena = 100,
                Kolicina = 100,
                Opis = "Opis",
                ZemljaPodrijetla = "Srbija",
                Vrsta = "Lager"
            };
            _pivoRepositoryMock.Setup(repo => repo.Update(It.IsAny<DbModels.Pivo>())).Returns((DbModels.Pivo)null);

            // Act
            var result = _controller.EditPivo(ime, pivo.ToDto());
            var resultType = result as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(resultType);
        }
    }
}
