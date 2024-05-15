using System;
using System.Collections.Generic;

namespace Pivoteka.DataAccess.PostgreSQL.Data.DbModels
{
    public partial class Zaposlenik
    {
        public DateOnly DatumZaposljenja { get; set; }
        public DateOnly? KrajZaposlenja { get; set; }
        public string KorisnickoIme { get; set; } = null!;

        public virtual Korisnik KorisnickoImeNavigation { get; set; } = null!;
    }
}
