using System;
using System.Collections.Generic;
using System.Data;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AplikacijaGlasovanje.ModelsUpdate
{
    public partial class OddaniOdgovori
    {
        public OddaniOdgovori()
        {
            Vprasanja = new HashSet<Vprasanja>();
        }
        public OddaniOdgovori(IDataReader reader)
        {
            OddaniOdgovoriId = (int)reader["OddaniOdgovoriId"];
            VprasanjeIzSeznamaId = (int)reader["VprasanjeIzSeznamaId"];
            OddaniOdgovor = (string)reader["OddaniOdgovor"];
            UporabnikId = (string)reader["UporabnikId"];
        }
        public OddaniOdgovori( int VprasanjeIzSzId, string oddan, string user)
        {
        
            VprasanjeIzSeznamaId = VprasanjeIzSzId;
            OddaniOdgovor = oddan;
            UporabnikId = user;
        }
        public int OddaniOdgovoriId { get; set; }
        public int VprasanjeIzSeznamaId { get; set; }
        public string OddaniOdgovor { get; set; }
        public string UporabnikId { get; set; }

        public virtual AspNetUsers Uporabnik { get; set; }
        public virtual SeznamVprasanj VprasanjeIzSeznama { get; set; }
        public virtual ICollection<Vprasanja> Vprasanja { get; set; }
        //--
        public virtual ICollection<Odgovori> Odgovori { get; set; }
    }
}
