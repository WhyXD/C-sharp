using System;
using System.Collections.Generic;
using System.Data;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AplikacijaGlasovanje.ModelsUpdate
{
    public partial class PravilniOdgovori
    {
        public PravilniOdgovori()
        {
            Vprasanja = new HashSet<Vprasanja>();
        }
        public PravilniOdgovori(IDataReader reader)
        {
            PravilniOdgovoriId = (int)reader["PravilniOdgovoriId"];
            VprasanjeIzSeznamaId = (int)reader["VprasanjeIzSeznamaId"];
            PravilniOdgovor = (string)reader["PravilniOdgovor"];
        }
        public int PravilniOdgovoriId { get; set; }
        public int VprasanjeIzSeznamaId { get; set; }
        public string PravilniOdgovor { get; set; }

        public virtual SeznamVprasanj VprasanjeIzSeznama { get; set; }
        public virtual ICollection<Vprasanja> Vprasanja { get; set; }
        //--
        public virtual ICollection<Odgovori> Odgovori { get; set; }
    }
}
