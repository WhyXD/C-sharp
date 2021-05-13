using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AplikacijaGlasovanje.ModelsUpdate
{
    public partial class Vprasanjas
    {
        public int Id { get; set; }
        public string Vprasanje { get; set; }
        public string OdgovorA { get; set; }
        public string OdgovorB { get; set; }
        public string OdgovorC { get; set; }
        public string OdgovorD { get; set; }
        public string PravilniOdgovor { get; set; }
        public string IzbranOdgovor { get; set; }
    }
}
