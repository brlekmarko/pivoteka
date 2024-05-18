using Pivoteka.Repositories;
using Microsoft.AspNetCore.Mvc;
using PivotekaWebApi.DTOs;
using DbModels = Pivoteka.DataAccess.PostgreSQL.Data.DbModels;
using Pivoteka.Commons;
using FakeItEasy;
using PivotekaWebApi.Controllers;
using FluentAssertions;
using Pivoteka.Repositories.PostgreSQL;
using System;

namespace PivotekaWebApi.UnitTests.Controllers
{
    public class NarudzbaControllerTests
    {
        private readonly INarudzbaRepository<int, DbModels.Narudzba> _narudzbaRepository;
        private readonly INarucioPivoRepository<int, DbModels.NarucioPivo> _narucioPivoRepository;
        private readonly NarudzbaController _controller;

        public NarudzbaControllerTests()
        {
            _narudzbaRepository = A.Fake<INarudzbaRepository<int, DbModels.Narudzba>>();
            _narucioPivoRepository = A.Fake<INarucioPivoRepository<int, DbModels.NarucioPivo>>();
            _controller = new NarudzbaController(_narudzbaRepository, _narucioPivoRepository);
        }

        [Fact]
        public void NarudzbaController_GetAllNarudzba_ReturnsOK()
        {
            // Arrange
            var narudzba = A.Fake<IEnumerable<DbModels.Narudzba>>();
            A.CallTo(() => _narudzbaRepository.GetAll()).Returns(narudzba);
            // Act
            var result = _controller.GetAllNarudzba();
            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void NarudzbaController_GetNarudzba_ReturnsOK()
        {
            // Arrange
            var narudzba = A.Fake<DbModels.Narudzba>();
            A.CallTo(() => _narudzbaRepository.Get(narudzba.Id)).Returns(Options.Some(narudzba));
            // Act
            var result = _controller.GetNarudzba(narudzba.Id);
            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void NarudzbaController_EditNarudzba_ReturnsNotFound()
        {
            // Arrange
            var narudzba = A.Fake<DbModels.Narudzba>();
            A.CallTo(() => _narudzbaRepository.Exists(narudzba.Id)).Returns(false);
            // Act
            var result = _controller.EditNarudzba(narudzba.Id, narudzba.ToDto());
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }


        [Fact]
        public void NarudzbaController_CreateNarudzba_ReturnsInternalServerError_WhenInsertionFails()
        {
            // Arrange
            var narudzba = A.Fake<DbModels.Narudzba>();
            A.CallTo(() => _narudzbaRepository.Exists(narudzba.Id)).Returns(false);
            A.CallTo(() => _narudzbaRepository.Insert(narudzba)).Returns(false);

            // Act
            var result = _controller.CreateNarudzba(narudzba.ToDto());

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<StatusCodeResult>().Which.StatusCode.Should().Be(500);
        }


        [Fact]
        public void NarudzbaController_DeleteNarudzba_ReturnsNoContent()
        {
            // Arrange
            var narudzba = A.Fake<DbModels.Narudzba>();
            A.CallTo(() => _narudzbaRepository.Exists(narudzba.Id)).Returns(true);
            A.CallTo(() => _narudzbaRepository.Remove(narudzba.Id)).Returns(true);
            // Act
            var result = _controller.DeleteNarudzba(narudzba.Id);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void NarudzbaController_DeleteNarudzba_ReturnsNotFound()
        {
            // Arrange
            var narudzba = A.Fake<DbModels.Narudzba>();
            A.CallTo(() => _narudzbaRepository.Exists(narudzba.Id)).Returns(false);
            // Act
            var result = _controller.DeleteNarudzba(narudzba.Id);
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }



    }
}