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
    public class PivoControllerTests : IDisposable
    {
        private CustomWebApplicatonFactory _factory;
        private HttpClient _client;

        public PivoControllerTests()
        {
            _factory = new CustomWebApplicatonFactory();
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task PivoController_GetAllPivo_ReturnsOK()
        {
            // Arrange
            var mockPive = new List<DbModels.Pivo>
            {
                new DbModels.Pivo
                {
                    Ime = "Ime",
                    Cijena = 1,
                    Kolicina = 1
                }
            }.AsQueryable();

            _factory.PivoRepositoryMock.Setup(x => x.GetAll()).Returns(mockPive);

            // Act
            var response = await _client.GetAsync("/api/Pivo");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var data = JsonConvert.DeserializeObject<IEnumerable<DbModels.Pivo>>(await response.Content.ReadAsStringAsync());
            Assert.Collection((data as IEnumerable<DbModels.Pivo>)!,
                item =>
                {
                    Assert.Equal("Ime", item.Ime);
                    Assert.Equal(1, item.Cijena);
                    Assert.Equal(1, item.Kolicina);
                });
        }

        [Fact]
        public async Task PivoController_GetPivo_ReturnsOK()
        {
            // Arrange
            var mockPivoList = new List<DbModels.Pivo>
            {
                new DbModels.Pivo
                {
                    Ime = "Ime",
                    Cijena = 1,
                    Kolicina = 1
                }
            };
            _factory.PivoRepositoryMock.Setup(x => x.GetAll()).Returns(mockPivoList);

            // Act
            var response = await _client.GetAsync("/api/Pivo");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var data = JsonConvert.DeserializeObject<List<DbModels.Pivo>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(data);
            Assert.Single(data);
            Assert.Equal("Ime", data[0].Ime);
            Assert.Equal(1, data[0].Cijena);
            Assert.Equal(1, data[0].Kolicina);
        }

        [Fact]
        public async Task PivoController_EditPivo_ReturnsBadRequest_WhenModelStateInvalid()
        {
            // Arrange
            var pivo = new DbModels.Pivo { Ime = "", Cijena = 1, Kolicina = 1 }; // Invalid ModelState due to empty name

            // Act
            var response = await _client.PutAsJsonAsync("/api/Pivo/Ime", pivo);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task PivoController_EditPivo_ReturnsBadRequest_WhenImeMismatch()
        {
            // Arrange
            var pivo = new DbModels.Pivo { Ime = "DifferentIme", Cijena = 1, Kolicina = 1 };

            // Act
            var response = await _client.PutAsJsonAsync("/api/Pivo/Ime", pivo);

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
