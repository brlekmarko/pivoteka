using Microsoft.EntityFrameworkCore;
using Pivoteka.DataAccess.PostgreSQL.Data;
using Pivoteka.DataAccess.PostgreSQL.Data.DbModels;
using Pivoteka.Repositories;
using Pivoteka.Repositories.PostgreSQL;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddDbContext<PivotekaContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("PivotekaContext")));
        builder.Services.AddTransient<IPivoRepository<string, Pivo>, PivoRepository>();
        builder.Services.AddTransient<INarudzbaRepository<int, Narudzba>, NarudzbaRepository>();
        builder.Services.AddTransient<IKorisnikRepository<string, Korisnik>, KorisnikRepository>();
        builder.Services.AddTransient<IVrstumRepository<string, Vrstum>, VrstumRepository>();
        builder.Services.AddTransient<IDobavljacRepository<string, Dobavljac>, DobavljacRepository>();
        builder.Services.AddTransient<INarucioPivoRepository<int, NarucioPivo>, NarucioPivoRepository>();


        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        builder.Services.AddCors(options => options.AddPolicy(name: "PivotekaOrigins",
            policy =>
            {
                policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));



        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("PivotekaOrigins");

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}