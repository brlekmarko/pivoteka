using System;
using System.Collections.Generic;

namespace Pivoteka.DataAccess.PostgreSQL.Data.DbModels
{
    public partial class Vrstum
    {
        public Vrstum()
        {
            Pivos = new HashSet<Pivo>();
        }

        public string Ime { get; set; } = null!;

        public virtual ICollection<Pivo> Pivos { get; set; }
    }
}
