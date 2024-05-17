using System.ComponentModel.DataAnnotations;
using DbModels = Pivoteka.DataAccess.PostgreSQL.Data.DbModels;

namespace PivotekaWebApi.DTOs;

public class Narudzba
{
    public int Id { get; set; }
    public DateTime Datum { get; set; }
    public decimal Ukupna_cijena { get; set; }

    [Required(ErrorMessage = "Name can't be null")]
    [StringLength(50, ErrorMessage = "Name can't be longer than 64 characters")]
    public string Korisnicko_ime { get; set; } = null!;
}


public static partial class DtoMapping
{
    public static Narudzba ToDto(this DbModels.Narudzba narudzba)
        => new Narudzba()
        {
            Id = narudzba.Id,
            Datum = narudzba.Datum,
            Ukupna_cijena = narudzba.UkupnaCijena,
            Korisnicko_ime = narudzba.KorisnickoIme
        };

    public static DbModels.Narudzba ToDbModel(this Narudzba narudzba)
        => new DbModels.Narudzba()
        {
            Id = narudzba.Id,
            Datum = narudzba.Datum.ToUniversalTime(),
            UkupnaCijena = narudzba.Ukupna_cijena,
            KorisnickoIme = narudzba.Korisnicko_ime
        };
}
