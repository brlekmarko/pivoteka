using System.ComponentModel.DataAnnotations;
using DbModels = Pivoteka.DataAccess.PostgreSQL.Data.DbModels;

namespace PivotekaWebApi.DTOs;

public class Pivo
{

    [Required(ErrorMessage = "Name can't be null")]
    [StringLength(50, ErrorMessage = "Name can't be longer than 64 characters")]
    public string Ime { get; set; } = string.Empty;
    public decimal Cijena { get; set; }
    public int Količina { get; set; }
    public string Opis { get; set; } = null!;

    [Required(ErrorMessage = "Zemlja podrijetla can't be null")]
    [StringLength(50, ErrorMessage = "Zemlja podrijetla  can't be longer than 64 characters")]
    public string ZemljaPodrijetla { get; set; } = null!;
    public int NetoVolumen { get; set; }

    [Required(ErrorMessage = "Dobavljac name can't be null")]
    [StringLength(50, ErrorMessage = "Dobavaljac name can't be longer than 64 characters")]
    public string ImeDobavljaca { get; set; } = null!;
    public string Vrsta { get; set; } = null!;
}


public static partial class DtoMapping
{
    public static Pivo ToDto(this DbModels.Pivo pivo)
        => new Pivo()
        {
            Ime = pivo.Ime,
            Cijena = pivo.Cijena,
            Količina = pivo.Količina,
            Opis = pivo.Opis,
            ZemljaPodrijetla = pivo.ZemljaPodrijetla,
            NetoVolumen = pivo.NetoVolumen,
            ImeDobavljaca = pivo.ImeDobavljaca,
            Vrsta = pivo.Vrsta

        };

    public static DbModels.Pivo ToDbModel(this Pivo pivo)
        => new DbModels.Pivo()
        {
            Ime = pivo.Ime,
            Cijena = pivo.Cijena,
            Količina = pivo.Količina,
            Opis = pivo.Opis,
            ZemljaPodrijetla = pivo.ZemljaPodrijetla,
            NetoVolumen = pivo.NetoVolumen,
            ImeDobavljaca = pivo.ImeDobavljaca,
            Vrsta = pivo.Vrsta
        };
}
