using Pivoteka.DataAccess.PostgreSQL.Data.DbModels;
using System.ComponentModel.DataAnnotations;
using DbModels = Pivoteka.DataAccess.PostgreSQL.Data.DbModels;

namespace PivotekaWebApi.DTOs;

public class NarudzbaAggregate
{

    public int Id { get; set; }
    public DateTime Datum { get; set; }
    public decimal Ukupna_cijena { get; set; }

    [Required(ErrorMessage = "Name can't be null")]
    [StringLength(50, ErrorMessage = "Name can't be longer than 64 characters")]
    public string Korisnicko_ime { get; set; } = null!;
    public IEnumerable<NarucioPivo> Stavke { get; set; } = Enumerable.Empty<NarucioPivo>();
}

public static partial class DtoMapping
{
    public static NarudzbaAggregate ToAggregateDto(this DbModels.Narudzba narudzba)
        => new NarudzbaAggregate()
        {
            Id = narudzba.Id,
            Datum = narudzba.Datum,
            Ukupna_cijena = narudzba.UkupnaCijena,
            Korisnicko_ime = narudzba.KorisnickoIme,
            Stavke = narudzba.NarucioPivos.Select(ns => ns.ToDto()).ToList()

        };

    public static DbModels.Narudzba ToDbModel(this NarudzbaAggregate narudzba)
        => new DbModels.Narudzba()
        {
            Id = narudzba.Id,
            Datum = narudzba.Datum.ToUniversalTime(),
            UkupnaCijena = narudzba.Ukupna_cijena,
            KorisnickoIme = narudzba.Korisnicko_ime,
            NarucioPivos = narudzba.Stavke.Select(ns => ns.ToDbModel()).ToList()
        };


}