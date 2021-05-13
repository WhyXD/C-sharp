using System;
using System.Collections.Generic;
using System.Data;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AplikacijaGlasovanje.ModelsUpdate
{
    public partial class Odgovori
    {
        public Odgovori()
        {
            Vprasanja = new HashSet<Vprasanja>();
        }
        public Odgovori(IDataReader reader)
        {
            OdgovoriId = (int)reader["OdgovoriId"];
            OddaniOdgovoriId = (int)reader["OddaniOdgovoriId"];
            PravilniOdgovoriId = (int)reader["PravilniOdgovoriId"];
            VprasanjeIzSeznamaId = (int)reader["VprasanjeIzSeznamaId"];

        }
        public Odgovori(IDataReader reader, string b)
        {
            idOdgovora = (int)reader["OdgovoriID"];
            VprasanjeIzSez = (string)reader["VprasanjeIzSeznama"];
            OddaniOdgo = (string)reader["OddaniOdgovor"];
            PravilniOdgo = (string)reader["PravilniOdgovor"];
            Odgovoril = (string)reader["UporabnikID"];
        }
        public int OdgovoriId { get; set; }
        public int OddaniOdgovoriId { get; set; }
        public int PravilniOdgovoriId { get; set; }
        public int VprasanjeIzSeznamaId { get; set; }
        //--
        public string VprasanjeIzSez { get; set; }
        public string OddaniOdgo { get; set; }
        public string PravilniOdgo { get; set; }
        public string Odgovoril { get; set; }
        public int idOdgovora { get; set; }

        //--

        public virtual SeznamVprasanj VprasanjeIzSeznama { get; set; }
        public virtual OddaniOdgovori OddaniOdgovori { get; set; }
        public virtual PravilniOdgovori PravilniOdgovori { get; set; }
        public virtual ICollection<Vprasanja> Vprasanja { get; set; }
       
    }
}
