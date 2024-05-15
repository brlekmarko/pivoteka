using System.ComponentModel.DataAnnotations;
using DbModels = Pivoteka.DataAccess.PostgreSQL.Data.DbModels;

namespace PivotekaWebApi.DTOs;

public class Vrstum
{

    [Required(ErrorMessage = "Name can't be null")]
    [StringLength(50, ErrorMessage = "Name can't be longer than 64 characters")]
    public string Ime { get; set; } = string.Empty;
}


public static partial class DtoMapping
{
    public static Vrstum ToDto(this DbModels.Vrstum vrsta)
        => new Vrstum()
        {
            Ime = vrsta.Ime

        };

    public static DbModels.Vrstum ToDbModel(this Vrstum vrsta)
        => new DbModels.Vrstum()
        {
            Ime = vrsta.Ime
        };
}
