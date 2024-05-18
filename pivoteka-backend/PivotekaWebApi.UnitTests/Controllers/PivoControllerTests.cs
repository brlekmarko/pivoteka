using Pivoteka.Repositories;
using Microsoft.AspNetCore.Mvc;
using PivotekaWebApi.DTOs;
using DbModels = Pivoteka.DataAccess.PostgreSQL.Data.DbModels;
using Pivoteka.Commons;
using FakeItEasy;
using PivotekaWebApi.Controllers;
using FluentAssertions;

namespace PivotekaWebApi.UnitTests.Controllers
{
    public class PivoControllerTests
    {
        private readonly IPivoRepository<string, DbModels.Pivo> _pivoRepository;
        private readonly PivoController _controller;

        public PivoControllerTests()
        {
            _pivoRepository = A.Fake<IPivoRepository<string, DbModels.Pivo>>();
            _controller = new PivoController(_pivoRepository);
        }


        [Fact]
        public void PivoController_GetAllPivo_ReturnsOK()
        {
            // Arrange
            var pivo = A.Fake<IEnumerable<DbModels.Pivo>>();
            A.CallTo(() => _pivoRepository.GetAll()).Returns(pivo);
            // Act
            var result = _controller.GetAllPivo();

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void PivoController_GetPivo_ReturnsOK()
        {
            // Arrange
            var pivo = A.Fake<DbModels.Pivo>();
            A.CallTo(() => _pivoRepository.Get(pivo.Ime)).Returns(Options.Some(pivo));

            // Act
            var result = _controller.GetPivo(pivo.Ime);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkObjectResult>();
        }


        [Fact]
        public void PivoController_EditPivo_ReturnsNotFound()
        {
            // Arrange
            var pivo = A.Fake<DbModels.Pivo>();
            A.CallTo(() => _pivoRepository.Exists(pivo.Ime)).Returns(false);
            // Act
            var result = _controller.EditPivo(pivo.Ime, pivo.ToDto());
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }

       

        [Fact]
        public void PivoController_CreatePivo_ReturnsInternalServerError_WhenInsertionFails()
        {
            // Arrange
            var pivo = A.Fake<DbModels.Pivo>();
            A.CallTo(() => _pivoRepository.Exists(pivo.Ime)).Returns(false);
            A.CallTo(() => _pivoRepository.Insert(pivo)).Returns(false);

            // Act
            var result = _controller.CreatePivo(pivo.ToDto());

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<StatusCodeResult>().Which.StatusCode.Should().Be(500);
        }


        [Fact]

        public void PivoController_DeletePivo_ReturnsNoContent()
        {
            // Arrange
            var pivo = A.Fake<DbModels.Pivo>();
            A.CallTo(() => _pivoRepository.Exists(pivo.Ime)).Returns(true);
            A.CallTo(() => _pivoRepository.Remove(pivo.Ime)).Returns(true);
            // Act
            var result = _controller.DeletePivo(pivo.Ime);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void PivoController_DeletePivo_ReturnsNotFound()
        {
            // Arrange
            var pivo = A.Fake<DbModels.Pivo>();
            A.CallTo(() => _pivoRepository.Exists(pivo.Ime)).Returns(false);
            // Act
            var result = _controller.DeletePivo(pivo.Ime);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }


    }
}