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
    public class DobavljacControllerTests
    {
        private readonly IDobavljacRepository<string, DbModels.Dobavljac> _dobavljacRepository;
        private readonly DobavljacController _controller;

        public DobavljacControllerTests()
        {
            _dobavljacRepository = A.Fake<IDobavljacRepository<string, DbModels.Dobavljac>>();
            _controller = new DobavljacController(_dobavljacRepository);
        }

        [Fact]
        public void DobavljacController_GetAllDobavljac_ReturnsOK()
        {
            // Arrange
            var dobavljaci = A.Fake<IEnumerable<DbModels.Dobavljac>>();
            A.CallTo(() => _dobavljacRepository.GetAll()).Returns(dobavljaci);
            // Act
            var result = _controller.GetAllDobavljac();
            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkObjectResult>();
        }



    }
}