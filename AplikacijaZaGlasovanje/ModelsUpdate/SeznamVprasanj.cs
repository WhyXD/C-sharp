using System.Data;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AplikacijaGlasovanje.ModelsUpdate
{
    public partial class SeznamVprasanj
    {
        public SeznamVprasanj()
        {
            MozniOdgovori = new HashSet<MozniOdgovori>();
            OddaniOdgovori = new HashSet<OddaniOdgovori>();
            Odgovori = new HashSet<Odgovori>();
            PravilniOdgovori = new HashSet<PravilniOdgovori>();
            Vprasanja = new HashSet<Vprasanja>();
        }
        public SeznamVprasanj(IDataReader reader)
        {
            SeznamVprasanjId = (int)reader["SeznamVprasanjId"];
            VprasanjeIzSeznama = (string)reader["VprasanjeIzSeznama"];
        }
        public int SeznamVprasanjId { get; set; }
        public string VprasanjeIzSeznama { get; set; }

        public virtual ICollection<MozniOdgovori> MozniOdgovori { get; set; }
        public virtual ICollection<OddaniOdgovori> OddaniOdgovori { get; set; }
        public virtual ICollection<Odgovori> Odgovori { get; set; }
        public virtual ICollection<PravilniOdgovori> PravilniOdgovori { get; set; }
        public virtual ICollection<Vprasanja> Vprasanja { get; set; }
    }
}
