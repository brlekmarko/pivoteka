using System.ComponentModel.DataAnnotations;
using DbModels = Pivoteka.DataAccess.PostgreSQL.Data.DbModels;

namespace PivotekaWebApi.DTOs;

public class Narudzba
{
    public int Id { get; set; }
    public DateTime Datum { get; set; }
    public decimal UkupnaCijena { get; set; }

    [Required(ErrorMessage = "Name can't be null")]
    [StringLength(50, ErrorMessage = "Name can't be longer than 64 characters")]
    public string KorisnickoIme { get; set; } = null!;
}


public static partial class DtoMapping
{
    public static Narudzba ToDto(this DbModels.Narudzba narudzba)
        => new Narudzba()
        {
            Id = narudzba.Id,
            Datum = narudzba.Datum,
            UkupnaCijena = narudzba.UkupnaCijena,
            KorisnickoIme = narudzba.KorisnickoIme
        };

    public static DbModels.Narudzba ToDbModel(this Narudzba narudzba)
        => new DbModels.Narudzba()
        {
            Id = narudzba.Id,
            Datum = narudzba.Datum,
            UkupnaCijena = narudzba.UkupnaCijena,
            KorisnickoIme = narudzba.KorisnickoIme
        };
}
