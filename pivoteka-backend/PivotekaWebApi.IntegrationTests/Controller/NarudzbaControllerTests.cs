using Microsoft.AspNetCore.Mvc;
using DbModels = Pivoteka.DataAccess.PostgreSQL.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using Pivoteka.Commons;
using System.Net.Http.Json;

namespace PivotekaWebApi.IntegrationTests.Controller
{
    public class NarudzbaControllerTests : IDisposable
    {
        private CustomWebApplicatonFactory _factory;
        private HttpClient _client;

        public NarudzbaControllerTests()
        {
            _factory = new CustomWebApplicatonFactory();
            _client = _factory.CreateClient();
        }



        [Fact]
        public async Task NarudzbaController_EditNarudzba_ReturnsBadRequest_WhenModelStateInvalid()
        {
            // Arrange
            var narudzba = new DbModels.Narudzba { Id = 1, Datum = DateTime.Now, UkupnaCijena = 1 }; // Invalid ModelState due to empty name

            // Act
            var response = await _client.PutAsJsonAsync("/api/Narudzba/Id", narudzba);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task NarudzbaController_EditNarudzba_ReturnsBadRequest_WhenImeMismatch()
        {
            // Arrange
            var narudzba = new DbModels.Narudzba { Id = 1, Datum = DateTime.Now, UkupnaCijena = 1 };

            // Act
            var response = await _client.PutAsJsonAsync("/api/Narudzba/Id", narudzba);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


        public void Dispose()
        {
            _client.Dispose();
            _factory.Dispose();
        }
    }
}
