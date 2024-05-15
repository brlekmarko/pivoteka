using System;
using System.Collections.Generic;

namespace Pivoteka.DataAccess.PostgreSQL.Data.DbModels
{
    public partial class Korisnik
    {
        public string KorisnickoIme { get; set; } = null!;
        public string Lozinka { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Ime { get; set; } = null!;
        public string Prezime { get; set; } = null!;

        public virtual Kupac Kupac { get; set; } = null!;
        public virtual Zaposlenik Zaposlenik { get; set; } = null!;
    }
}
