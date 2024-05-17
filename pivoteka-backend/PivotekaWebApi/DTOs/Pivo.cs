using System.ComponentModel.DataAnnotations;
using DbModels = Pivoteka.DataAccess.PostgreSQL.Data.DbModels;

namespace PivotekaWebApi.DTOs;

public class Pivo
{

    [Required(ErrorMessage = "Name can't be null")]
    [StringLength(50, ErrorMessage = "Name can't be longer than 64 characters")]
    public string Ime { get; set; } = string.Empty;
    public decimal Cijena { get; set; }
    public int Kolicina { get; set; }
    public string Opis { get; set; } = null!;

    [Required(ErrorMessage = "Zemlja podrijetla can't be null")]
    [StringLength(50, ErrorMessage = "Zemlja podrijetla  can't be longer than 64 characters")]
    public string Zemlja_Podrijetla { get; set; } = null!;
    public int Neto_volumen { get; set; }

    [Required(ErrorMessage = "Dobavljac name can't be null")]
    [StringLength(50, ErrorMessage = "Dobavaljac name can't be longer than 64 characters")]
    public string Ime_Dobavljaca { get; set; } = null!;
    public string Vrsta { get; set; } = null!;
}


public static partial class DtoMapping
{
    public static Pivo ToDto(this DbModels.Pivo pivo)
        => new Pivo()
        {
            Ime = pivo.Ime,
            Cijena = pivo.Cijena,
            Kolicina = pivo.Kolicina,
            Opis = pivo.Opis,
            Zemlja_Podrijetla = pivo.ZemljaPodrijetla,
            Neto_volumen = pivo.NetoVolumen,
            Ime_Dobavljaca = pivo.ImeDobavljaca,
            Vrsta = pivo.Vrsta

        };

    public static DbModels.Pivo ToDbModel(this Pivo pivo)
        => new DbModels.Pivo()
        {
            Ime = pivo.Ime,
            Cijena = pivo.Cijena,
            Kolicina = pivo.Kolicina,
            Opis = pivo.Opis,
            ZemljaPodrijetla = pivo.Zemlja_Podrijetla,
            NetoVolumen = pivo.Neto_volumen,
            ImeDobavljaca = pivo.Ime_Dobavljaca,
            Vrsta = pivo.Vrsta
        };
}
