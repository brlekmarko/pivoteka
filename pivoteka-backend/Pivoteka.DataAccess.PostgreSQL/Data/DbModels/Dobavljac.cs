using System;
using System.Collections.Generic;

namespace Pivoteka.DataAccess.PostgreSQL.Data.DbModels
{
    public partial class Dobavljac
    {
        public Dobavljac()
        {
            Pivos = new HashSet<Pivo>();
        }

        public string Ime { get; set; } = null!;
        public string Adresa { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Pivo> Pivos { get; set; }
    }
}
