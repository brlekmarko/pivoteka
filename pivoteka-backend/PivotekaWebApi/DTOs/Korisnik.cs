using Pivoteka.DataAccess.PostgreSQL.Data.DbModels;
using System.ComponentModel.DataAnnotations;
using DbModels = Pivoteka.DataAccess.PostgreSQL.Data.DbModels;

namespace PivotekaWebApi.DTOs;

public class Korisnik
{

    [Required(ErrorMessage = "Name can't be null")]
    [StringLength(32, ErrorMessage = "Name can't be longer than 32 characters")]
    public string KorisnickoIme { get; set; } = null!;

    [Required(ErrorMessage = "Password can't be null")]
    [StringLength(256, ErrorMessage = "Password can't be longer than 256 characters")]
    public string Lozinka { get; set; } = null!;
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "First name can't be null")]
    [StringLength(64, ErrorMessage = "First name can't be longer than 64 characters")]
    public string Ime { get; set; } = null!;

    [Required(ErrorMessage = "Last name can't be null")]
    [StringLength(64, ErrorMessage = "Last name can't be longer than 64 characters")]
    public string Prezime { get; set; } = null!;
}


public static partial class DtoMapping
{
    public static Korisnik ToDto(this DbModels.Korisnik korisnik)
        => new Korisnik()
        {
            KorisnickoIme = korisnik.KorisnickoIme,
            Lozinka = korisnik.Lozinka,
            Email = korisnik.Email,
            Ime = korisnik.Ime,
            Prezime = korisnik.Prezime
        };

    public static DbModels.Korisnik ToDbModel(this Korisnik korisnik)
        => new DbModels.Korisnik()
        {
            Ime = korisnik.Ime,
            KorisnickoIme = korisnik.KorisnickoIme,
            Lozinka = korisnik.Lozinka,
            Email = korisnik.Email,
            Prezime = korisnik.Prezime
        };
}


