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
    public class VrstumControllerTests
    {
        private readonly IVrstumRepository<string, DbModels.Vrstum> _vrstumRepository;
        private readonly VrstumController _controller;

        public VrstumControllerTests()
        {
            _vrstumRepository = A.Fake<IVrstumRepository<string, DbModels.Vrstum>>();
            _controller = new VrstumController(_vrstumRepository);
        }

        [Fact]
        public void VrstumController_GetAllVrstum_ReturnsOK()
        {
            // Arrange
            var vrste = A.Fake<IEnumerable<DbModels.Vrstum>>();
            A.CallTo(() => _vrstumRepository.GetAll()).Returns(vrste);
            // Act
            var result = _controller.GetAllVrstum();
            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkObjectResult>();
        }



    }
}