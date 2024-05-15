using System;
using System.Collections.Generic;

namespace Pivoteka.DataAccess.PostgreSQL.Data.DbModels
{
    public partial class Kupac
    {
        public Kupac()
        {
            Narudzbas = new HashSet<Narudzba>();
            Ocjenas = new HashSet<Ocjena>();
        }

        public string AdresaDostave { get; set; } = null!;
        public string KorisnickoIme { get; set; } = null!;

        public virtual Korisnik KorisnickoImeNavigation { get; set; } = null!;
        public virtual ICollection<Narudzba> Narudzbas { get; set; }
        public virtual ICollection<Ocjena> Ocjenas { get; set; }
    }
}
