using System.ComponentModel.DataAnnotations;
using DbModels = Pivoteka.DataAccess.PostgreSQL.Data.DbModels;

namespace PivotekaWebApi.DTOs;

public class Dobavljac
{

    [Required(ErrorMessage = "Name can't be null")]
    [StringLength(50, ErrorMessage = "Name can't be longer than 64 characters")]
    public string Ime { get; set; } = string.Empty;
}


public static partial class DtoMapping
{
    public static Dobavljac ToDto(this DbModels.Dobavljac dobavljac)
        => new Dobavljac()
        {
            Ime = dobavljac.Ime

        };

    public static DbModels.Dobavljac ToDbModel(this Dobavljac dobavljac)
        => new DbModels.Dobavljac()
        {
            Ime = dobavljac.Ime
        };
}
