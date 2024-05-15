using System;
using System.Collections.Generic;

namespace Pivoteka.DataAccess.PostgreSQL.Data.DbModels
{
    public partial class Pivo
    {
        public Pivo()
        {
            NarucioPivos = new HashSet<NarucioPivo>();
            Ocjenas = new HashSet<Ocjena>();
        }

        public string Ime { get; set; } = null!;
        public decimal Cijena { get; set; }
        public int Količina { get; set; }
        public string Opis { get; set; } = null!;
        public string ZemljaPodrijetla { get; set; } = null!;
        public int NetoVolumen { get; set; }
        public string ImeDobavljaca { get; set; } = null!;
        public string Vrsta { get; set; } = null!;

        public virtual Dobavljac ImeDobavljacaNavigation { get; set; } = null!;
        public virtual Vrstum VrstaNavigation { get; set; } = null!;
        public virtual ICollection<NarucioPivo> NarucioPivos { get; set; }
        public virtual ICollection<Ocjena> Ocjenas { get; set; }
    }
}
