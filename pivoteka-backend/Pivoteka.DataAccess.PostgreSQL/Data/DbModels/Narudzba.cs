using System;
using System.Collections.Generic;

namespace Pivoteka.DataAccess.PostgreSQL.Data.DbModels
{
    public partial class Narudzba
    {
        public Narudzba()
        {
            NarucioPivos = new HashSet<NarucioPivo>();
        }

        public int Id { get; set; }
        public DateTime Datum { get; set; }
        public decimal UkupnaCijena { get; set; }
        public string KorisnickoIme { get; set; } = null!;

        public virtual Kupac KorisnickoImeNavigation { get; set; } = null!;
        public virtual ICollection<NarucioPivo> NarucioPivos { get; set; }
    }
}
