using System;
using System.Collections.Generic;

namespace Pivoteka.DataAccess.PostgreSQL.Data.DbModels
{
    public partial class NarucioPivo
    {
        public int Količina { get; set; }
        public decimal CijenaPiva { get; set; }
        public string ImePiva { get; set; } = null!;
        public int IdNarudzbe { get; set; }

        public virtual Narudzba IdNarudzbeNavigation { get; set; } = null!;
        public virtual Pivo ImePivaNavigation { get; set; } = null!;
    }
}
