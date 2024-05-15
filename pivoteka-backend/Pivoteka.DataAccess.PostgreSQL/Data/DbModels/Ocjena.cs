using System;
using System.Collections.Generic;

namespace Pivoteka.DataAccess.PostgreSQL.Data.DbModels
{
    public partial class Ocjena
    {
        public int Ocjena1 { get; set; }
        public string? Tekst { get; set; }
        public string KorisnickoImeKupca { get; set; } = null!;
        public string ImePiva { get; set; } = null!;

        public virtual Pivo ImePivaNavigation { get; set; } = null!;
        public virtual Kupac KorisnickoImeKupcaNavigation { get; set; } = null!;
    }
}
