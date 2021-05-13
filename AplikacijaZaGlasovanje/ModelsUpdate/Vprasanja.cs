using System;
using System.Collections.Generic;
using System.Data;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AplikacijaGlasovanje.ModelsUpdate
{
    public partial class Vprasanja
    {
        public Vprasanja(IDataReader reader)
        {
            VprasanjaId = (int)reader["VprasanjaId"];
            VprasanjeIzSeznamaId = (int)reader["VprasanjeIzSeznamaId"];
            MozniOdgovoriId = (int)reader["MozniOdgovoriId"];
            PravilniOdgovoriId = (int)reader["PravilniOdgovoriId"];
            GlasovanjeId = (int)reader["GlasovanjeId"];
            OddaniOdgovoriId = (int)reader["OddaniOdgovoriId"];
            OdgovoriId = (int)reader["OdgovoriId"];
        }
        public int VprasanjaId { get; set; }
        public int VprasanjeIzSeznamaId { get; set; }
        public int MozniOdgovoriId { get; set; }
        public int PravilniOdgovoriId { get; set; }
        public int GlasovanjeId { get; set; }
        public int OddaniOdgovoriId { get; set; }
        public int OdgovoriId { get; set; }

        public virtual Glasovanja Glasovanje { get; set; }
        public virtual MozniOdgovori MozniOdgovori { get; set; }
        public virtual OddaniOdgovori OddaniOdgovori { get; set; }
        public virtual Odgovori Odgovori { get; set; }
        public virtual PravilniOdgovori PravilniOdgovori { get; set; }
        public virtual SeznamVprasanj VprasanjeIzSeznama { get; set; }
    }
}
