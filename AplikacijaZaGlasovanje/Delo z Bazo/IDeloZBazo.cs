
using AplikacijaGlasovanje.ModelsUpdate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AplikacijaGlasovanje.Delo_z_Bazo
{
    public interface IDeloZBazo
    {
        string ConnectionStringName { get; set; }

        List<AspNetUsers> GetAspNetUsers();
        List<SeznamVprasanj> GetSeznamVprasanj();
        List<MozniOdgovori> GetMozneOdgovoreGledeNaIDVprasanjeIzSeznama(int SeznamVprasanjID);
        List<PravilniOdgovori> GetPravilneOdgovore(int seznamVprasanjID);
        List<Odgovori> GetOdgovori();
        List<Glasovanja> GetGlasovanja();
        void InsertVprasanjeVSeznamVprasanj(string vprasanje);
        void InsertMozenOdgovor(string mozenOdgovor);
        void InsertPravilniOdgovor(int idVprasanja, string pravilniOdgovor);
        void InsertOddaniOdgovor(int idVprasanja, string oddan, string uporabnik);
        void InsertOdgovori(int VprasanjeIzSeznamaID);
        List<AspNetUsers> GetTrenutnoPrijaavljenega();
        int indexVprasanjaIzSeznama(int i);
        int IndexZadnjegaVprasanjaVSeznamuVprasanj();
        int indexPravilnegaOdgovoraGledeNaIDVprasanja(int i);
        List<PravilniOdgovori> GetPravilneOdgovore();
        List<OddaniOdgovori> GetOddaneOdgovore();
        List<OddaniOdgovori> GetOddaneOdgovore(int seznamVprasanjID);
        List<AspNetUserRoles> GetUsersAndRoles();
        string GetRoleBasedOnIdUserja(string user);
        void InsertRolesOnUser(string user, string role);
        void PosodobiPravice(string user, int pravice);
    }
}