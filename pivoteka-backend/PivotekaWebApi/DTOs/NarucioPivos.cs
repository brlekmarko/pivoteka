using System.ComponentModel.DataAnnotations;
using System.Data;
using DbModels = Pivoteka.DataAccess.PostgreSQL.Data.DbModels;
namespace PivotekaWebApi.DTOs;

public class NarucioPivo
{
    public int Kolicina { get; set; }
    public decimal Cijena_piva { get; set; }
    public string Ime_piva { get; set; } = null!;
    public int Id_narudzbe { get; set; }
}


public static partial class DtoMapping
{
    public static NarucioPivo ToDto(this DbModels.NarucioPivo narucioPivo)
        => new NarucioPivo()
        {
            Kolicina = narucioPivo.Kolicina,
            Cijena_piva = narucioPivo.CijenaPiva,
            Ime_piva = narucioPivo.ImePiva,
            Id_narudzbe = narucioPivo.IdNarudzbe
        };

    public static DbModels.NarucioPivo ToDbModel(this NarucioPivo narucioPivo)
        => new DbModels.NarucioPivo()
        {
            Kolicina = narucioPivo.Kolicina,
            CijenaPiva = narucioPivo.Cijena_piva,
            ImePiva = narucioPivo.Ime_piva,
            IdNarudzbe = narucioPivo.Id_narudzbe
        };
}