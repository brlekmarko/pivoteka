using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using DbModels = Pivoteka.DataAccess.PostgreSQL.Data.DbModels;
using Pivoteka.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace PivotekaWebApi.IntegrationTests.Controller
{
    public class CustomWebApplicatonFactory : WebApplicationFactory<Program>
    {
        public Mock<IPivoRepository<string, DbModels.Pivo>> PivoRepositoryMock { get; }

        public Mock<INarudzbaRepository<string, DbModels.Narudzba>> NarudzbaRepositoryMock { get; }

        public CustomWebApplicatonFactory()
        {
            PivoRepositoryMock = new Mock<IPivoRepository<string, DbModels.Pivo>>();
            NarudzbaRepositoryMock = new Mock<INarudzbaRepository<string, DbModels.Narudzba>>();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton(PivoRepositoryMock.Object);
                services.AddSingleton(NarudzbaRepositoryMock.Object);
            });
        }
    }
}
