using System;
using System.Collections.Generic;
using System.Data;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AplikacijaGlasovanje.ModelsUpdate
{
    public partial class Glasovanja
    {
        public Glasovanja()
        {
            Vprasanja = new HashSet<Vprasanja>();
        }
        public Glasovanja(IDataReader reader)
        {
            GlasovanjeId = (int)reader["GlasovanjeId"];
            ImeGlasovanja = (string)reader["ImeGlasovanja"];
            GlasovanjeSeZacne = (DateTime)reader["GlasovanjeSeZacne"];
            GlasovanjeSeKonca = (DateTime)reader["GlasovanjeSeKonca"];
            UporabnikiId = (string)reader["UporabnikiId"];
            VprasanjeId = (int)reader["VprasanjeId"];
        }

        public int GlasovanjeId { get; set; }
        public string ImeGlasovanja { get; set; }
        public DateTime GlasovanjeSeZacne { get; set; }
        public DateTime GlasovanjeSeKonca { get; set; }
        public string UporabnikiId { get; set; }
        public int VprasanjeId { get; set; }

        public virtual AspNetUsers Uporabniki { get; set; }
        public virtual ICollection<Vprasanja> Vprasanja { get; set; }
    }
}
