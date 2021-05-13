using System;
using System.Collections.Generic;
using System.Data;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AplikacijaGlasovanje.ModelsUpdate
{
    public partial class MozniOdgovori
    {
        public MozniOdgovori()
        {
            Vprasanja = new HashSet<Vprasanja>();
        }
        public MozniOdgovori(IDataReader reader)
        {
            MozenOdgovorId = (int)reader["MozenOdgovorID"];
            VprasanjeIzSeznamaId = (int)reader["VprasanjeIzSeznamaID"];
            Odgovor = (string)reader["Odgovor"];
        }
        public MozniOdgovori(/*int moznegaOdgovora,*/ int idSeznama, string mozenOdgovor)
        {
           // MozenOdgovorId = (int)moznegaOdgovora;
            VprasanjeIzSeznamaId = (int) idSeznama;
            Odgovor = (string)mozenOdgovor;
        }
        public int MozenOdgovorId { get; set; }
        public int VprasanjeIzSeznamaId { get; set; }
        public string Odgovor { get; set; }

        public virtual SeznamVprasanj VprasanjeIzSeznama { get; set; }
        public virtual ICollection<Vprasanja> Vprasanja { get; set; }
    }
}
