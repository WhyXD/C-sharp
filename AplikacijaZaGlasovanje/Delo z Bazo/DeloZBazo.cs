using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AplikacijaGlasovanje.ModelsUpdate;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AplikacijaGlasovanje.Delo_z_Bazo
{
    public class DeloZBazo :IDeloZBazo
    {
        private VoteContext _context;
        private string ConnectionStringName { get; set; } = "povezava";
        string IDeloZBazo.ConnectionStringName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private readonly IConfiguration _config;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeloZBazo(VoteContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
        public DeloZBazo() { }
        public DeloZBazo(IHttpContextAccessor httpContextAccessor, VoteContext context, IConfiguration config)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _config = config;
        }

        private SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection
            {
                ConnectionString = _config.GetConnectionString(ConnectionStringName)
            };
            conn.Open();
            return conn;
        }
        public List<AspNetUsers> GetAspNetUsers()
        {
            var con = GetConnection();
            string sql = "Select * from [Vote].[dbo].[AspNetUsers]";
            SqlCommand cmd = new SqlCommand(sql,con);
            var reader = cmd.ExecuteReader();

            List<AspNetUsers> user = new  List<AspNetUsers>();
            while (reader.Read())
            {
                user.Add(new AspNetUsers(reader));
            }
          
            return  user;
        }
        public List<AspNetUsers> GetAspNetUsers(string username)
        {
            var con = GetConnection();
            string sql = "SELECT * FROM [Vote].[dbo].[AspNetUsers] WHERE UserName = @Username ";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Username", username);
            var reader = cmd.ExecuteReader();

            List<AspNetUsers> user = new List<AspNetUsers>();
            while (reader.Read())
            {
                user.Add(new AspNetUsers(reader));
            }

            return user;
        }
        public List<SeznamVprasanj> GetSeznamVprasanj()
        {
            var con = GetConnection();
            /*string sql = "Select  * from Vprasanja v, SeznamVprasanj s " +
                "where v.VprasanjeIzSeznamaID = s.SeznamVprasanjID  AND s.SeznamVprasanjID = 1" +
                "(SELECT  VprasanjeIzSeznama from SeznamVprasanj)";*/

            string sql = "SELECT * FROM SeznamVprasanj";
            SqlCommand cmd = new SqlCommand(sql, con);
            var reader = cmd.ExecuteReader();

            List<SeznamVprasanj> seznam = new List<SeznamVprasanj>();
            while (reader.Read())
            {
                seznam.Add(new SeznamVprasanj(reader));
            }

            return seznam;
        }
        public string VprasanjeIzSeznamaVprasanj(int SeznamVprasanjaID)
        {
            List<SeznamVprasanj> seznam =  GetSeznamVprasanj();
            string vp = "";
            foreach (var v in seznam)
            {
                if(v.SeznamVprasanjId == SeznamVprasanjaID)
                {
                        vp = v.VprasanjeIzSeznama;
                }    
            }
            return vp;

        }
       /*public async Task<List<Vprasanja>> PovezovalnaTabela()
        {
            var con = GetConnection();
            string sql = "SELECT VprasanjeIzSeznamaID FROM Vprasanja WHERE (VprasanjeIzSeznama, MozenOdgovorID) IN" +
                "(SELECT VprasanjeIzSeznama FROM SeznamVprasanj";
        }*/
       public List<MozniOdgovori> GetMozneOdgovoreGledeNaIDVprasanjeIzSeznama(int SeznamVprasanjID)
        {
            var con = GetConnection();
            string sql = "SELECT  MozenOdgovorID, Odgovor ,VprasanjeIzSeznamaID " +
                "FROM MozniOdgovori " +
                "WHERE(VprasanjeIzSeznamaID = @VprasanjeIzSeznamaID)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@VprasanjeIzSeznamaID", SeznamVprasanjID);
            var reader = cmd.ExecuteReader();

            List<MozniOdgovori> mozni = new List<MozniOdgovori>();
            while (reader.Read())
            {
                mozni.Add(new MozniOdgovori(reader));
            }
           
            return mozni;
        }

       public List<PravilniOdgovori> GetPravilneOdgovore(int seznamVprasanjID)
        {
            var con = GetConnection();
            string sql = "SELECT PravilniOdgovorID, PravilniOdgovor " +
                "FROM PravilniOdgovori" +
                "WHERE (VprasanjeIzSeznamaID = @VprasanjeIzSeznamaID)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@VprasanjeIzSeznamaID", seznamVprasanjID);
            var reader = cmd.ExecuteReader();
           
            List<PravilniOdgovori> prav = new List<PravilniOdgovori>();
            while (reader.Read())
            {
                prav.Add(new PravilniOdgovori(reader));
            }
            return prav;
        }
        public List<PravilniOdgovori> GetPravilneOdgovore()
        {
            var con = GetConnection();
        string sql = "SELECT PravilniOdgovoriID, PravilniOdgovor, VprasanjeIzSeznamaID " +
            "FROM PravilniOdgovori";
            SqlCommand cmd = new SqlCommand(sql, con);
            
            var reader = cmd.ExecuteReader();

            List<PravilniOdgovori> prav = new List<PravilniOdgovori>();
            while (reader.Read())
            {
                prav.Add(new PravilniOdgovori(reader));
            }
            return prav;
        }
        public List<OddaniOdgovori> GetOddaneOdgovore(int seznamVprasanjID)
        {
            var con = GetConnection();
            string sql = "SELECT OddaniOdgovoriID, OddaniOdgovor, UporabnikID ,VprasanjeIzSeznamaID " +
                "FROM OddaniOdgovori " +
                "WHERE (VprasanjeIzSeznamaID = @VprasanjeIzSeznamaID)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@VprasanjeIzSeznamaID", seznamVprasanjID);
            var reader = cmd.ExecuteReader();

            List<OddaniOdgovori> oddani = new List<OddaniOdgovori>();
            while (reader.Read())
            {
                oddani.Add(new OddaniOdgovori(reader));
            }
            return oddani;
            }
            public List<OddaniOdgovori> GetOddaneOdgovore()
            {
                var con = GetConnection();
                string sql = "SELECT OddaniOdgovoriID, OddaniOdgovor, UporabnikID, VprasanjeIzSeznamaID " +
                    "FROM OddaniOdgovori";
                SqlCommand cmd = new SqlCommand(sql, con);
      
                var reader = cmd.ExecuteReader();

                List<OddaniOdgovori> oddani = new List<OddaniOdgovori>();
                while (reader.Read())
                {
                    oddani.Add(new OddaniOdgovori(reader));
                }
                return oddani;
            }
       /* public List<OddaniOdgovori> GetOddaneOdgovore(int idVprasanja)
        {
            var con = GetConnection();
            string sql = "SELECT OddaniOdgovoriID, OddaniOdgovor, UporabnikID, VprasanjeIzSeznamaID " +
                "FROM OddaniOdgovori "+
                "WHERE (VprasanjeIzSeznamaID = @VprasanjeIzSeznamaID)" ;
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@VprasanjeIzSeznamaID", idVprasanja);
            var reader = cmd.ExecuteReader();

            List<OddaniOdgovori> oddani = new List<OddaniOdgovori>();
            while (reader.Read())
            {
                oddani.Add(new OddaniOdgovori(reader));
            }
            return oddani;
        }*/
        public List<Odgovori> GetOdgovori()
        {
            string sql2 = "SELECT  SeznamVprasanj.VprasanjeIzSeznama ,OddaniOdgovori.OddaniOdgovor , PravilniOdgovori.PravilniOdgovor ,OddaniOdgovori.UporabnikID , Odgovori.OdgovoriID " +
                "FROM Odgovori "+
                " INNER JOIN "+"SeznamVprasanj   ON (Odgovori.VprasanjeIzSeznamaID = SeznamVprasanj.SeznamVprasanjID) " +
                " INNER JOIN "+"OddaniOdgovori   ON (Odgovori.OddaniOdgovoriID     = OddaniOdgovori.OddaniOdgovoriID) " +
                " INNER JOIN "+"PravilniOdgovori ON (Odgovori.PravilniOdgovoriID   = PravilniOdgovori.PravilniOdgovoriID)";
             
             var con = GetConnection();
             SqlCommand cmd = new SqlCommand(sql2, con);
             var reader = cmd.ExecuteReader();

            List<Odgovori> od = new List<Odgovori>();
            while (reader.Read())
            {
                od.Add(new Odgovori(reader, ""));           
            }           
            return od;
        }
        
        public List<Glasovanja> GetGlasovanja()
        {
            var con = GetConnection();
            string sql = "SELECT * " +
                "FROM Glasovanja";
            SqlCommand cmd = new SqlCommand(sql, con);
            var reader = cmd.ExecuteReader();
            List<Glasovanja> glas = new List<Glasovanja>();

            while (reader.Read())
            {
                glas.Add(new Glasovanja(reader));
            }
            return glas;
        }
        public void InsertVprasanjeVSeznamVprasanj(string vprasanje)
        {
            var con = GetConnection();
            string sql = "INSERT INTO SeznamVprasanj (VprasanjeIzSeznama) " +
                "Values (@VprasanjeIzSeznama)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@VprasanjeIzSeznama", vprasanje);
            cmd.ExecuteNonQuery();
        }
        public void InsertMozenOdgovor(string mozenOdgovor)
        {
            var con = GetConnection();
            string sql ="INSERT INTO MozniOdgovori (VprasanjeIzSeznamaID, Odgovor) " +
                "values (@VprasanjeIzSeznamaID, @Odgovor)";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@VprasanjeIzSeznamaID", IndexZadnjegaVprasanjaVSeznamuVprasanj());
            cmd.Parameters.AddWithValue("@Odgovor", mozenOdgovor);
            cmd.ExecuteNonQuery();
          //  mozenOdgovor.MozenOdgovorId = (int)cmd.ExecuteScalar();
        }
        public void InsertPravilniOdgovor(int idVprasanja, string pravilniOdgovor)
        {
            var con = GetConnection();
            string sql = "INSERT INTO PravilniOdgovori (VprasanjeIzSeznamaID, PravilniOdgovor) " +
                "values (@VprasanjeIzSeznamaID, @PravilniOdgovor)";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@VprasanjeIzSeznamaID", indexVprasanjaIzSeznama(idVprasanja));
            cmd.Parameters.AddWithValue("@PravilniOdgovor", pravilniOdgovor);
            cmd.ExecuteNonQuery();
        }
        public void InsertOddaniOdgovor(int idVprasanja, string oddan, string uporabnik)
        {
            var con = GetConnection();
            string sql = "INSERT INTO OddaniOdgovori (VprasanjeIzSeznamaID, OddaniOdgovor, UporabnikID) " +
                "values (@VprasanjeIzSeznamaID, @OddaniOdgovor, @UporabnikID)";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.AddWithValue("@VprasanjeIzSeznamaID", indexVprasanjaIzSeznama(idVprasanja));
            cmd.Parameters.AddWithValue("@OddaniOdgovor", oddan);
            cmd.Parameters.AddWithValue("@UporabnikID", uporabnik);
            cmd.ExecuteNonQuery();
        }
        public void InsertOdgovori(int VprasanjeIzSeznamaID)
        {
            var con = GetConnection();
            string sql = "INSERT INTO Odgovori (OddaniOdgovoriID, PravilniOdgovoriID, VprasanjeIzSeznamaID) " +
                "values (@OddaniOdgovoriID, @PravilniOdgovoriID, @VprasanjeIzSeznamaID)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@OddaniOdgovoriID", IndexZadnjegaOddangeOdgovora());
            cmd.Parameters.AddWithValue("@PravilniOdgovoriID", indexPravilnegaOdgovoraGledeNaIDVprasanja(VprasanjeIzSeznamaID));
            cmd.Parameters.AddWithValue("@VprasanjeIzSeznamaID", indexVprasanjaIzSeznama(VprasanjeIzSeznamaID));
            cmd.ExecuteNonQuery();
        }
        public int IndexZadnjegaOddangeOdgovora()
        {
            string sql = "SELECT TOP(1) * FROM OddaniOdgovori " +
                "ORDER BY " + "OddaniOdgovoriID DESC";
            var con = GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            var reader = cmd.ExecuteReader();
            List<OddaniOdgovori> sz = new List<OddaniOdgovori>();

            while (reader.Read())
            {
                sz.Add(new OddaniOdgovori(reader));
            }

            PropertyInfo i = sz[0].GetType().GetProperty("OddaniOdgovoriId");
            return (int)(i.GetValue(sz[0], null));
        }
            public int indexPravilnegaOdgovoraGledeNaIDVprasanja(int idVprasanja)
        {
            var con = GetConnection();
            string sql = "SELECT * FROM PravilniOdgovori WHERE VprasanjeIzSeznamaID = @VprasanjeIzSeznamaID";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@VprasanjeIzSeznamaID", idVprasanja);
            List<PravilniOdgovori> pr = new List<PravilniOdgovori>();
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                pr.Add(new PravilniOdgovori(reader));
            }
            PropertyInfo inf = pr[0].GetType().GetProperty("PravilniOdgovoriId");
            return (int)(inf.GetValue(pr[0], null));
        }
        public List<AspNetUsers> GetTrenutnoPrijaavljenega()
        {
            return GetAspNetUsers(_httpContextAccessor.HttpContext.User.Identity.Name);
        }
        public int IndexZadnjegaVprasanjaVSeznamuVprasanj()
        {
            string sql = "SELECT TOP(1) * FROM SeznamVprasanj " +
                "ORDER BY "+"SeznamVprasanjID DESC";
            var con = GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            var reader = cmd.ExecuteReader();
            List<SeznamVprasanj> sz = new List<SeznamVprasanj>();
            
            while (reader.Read())
            {
                sz.Add(new SeznamVprasanj(reader));                
            }

            PropertyInfo i = sz[0].GetType().GetProperty("SeznamVprasanjId");
            return (int)(i.GetValue(sz[0], null));           
        }
        public int indexVprasanjaIzSeznama(int i)
        {
            string sql = "SELECT * FROM SeznamVprasanj WHERE SeznamVprasanjID = @SeznamVprasanjID";
            var con = GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@SeznamVprasanjID", i);
            var reader = cmd.ExecuteReader();
            List<SeznamVprasanj> sz = new List<SeznamVprasanj>();

            while (reader.Read())
            {
                sz.Add(new SeznamVprasanj(reader));
            }

            PropertyInfo inf = sz[0].GetType().GetProperty("SeznamVprasanjId");
            return (int)(inf.GetValue(sz[0], null));
        }
        public void InsertOdgovori(int oddaniID, int pravilniID, int seznamID)
        {
            throw new NotImplementedException();
        }
        public List<AspNetUserRoles> GetUsersAndRoles()
        {
            string sql = "SELECT AspNetUsers.Id , AspNetRoles.Name " +
                   "FROM AspNetUserRoles " +
                    "INNER JOIN " + "AspNetUsers ON (AspNetUserRoles.UserId = AspNetUsers.Id)" +
                    "INNER JOIN " + "AspNetRoles ON (AspNetUserRoles.RoleId = AspNetRoles.Id)";
              //      "WHERE (AspNetRoles.Name = 'admin')";
            var conn = GetConnection();

            SqlCommand cmd = new SqlCommand(sql, conn);
            var reader = cmd.ExecuteReader();
            List<AspNetUserRoles> role = new List<AspNetUserRoles>();
            while (reader.Read())
            {
                role.Add(new AspNetUserRoles(reader,null));
            }
           // Console.WriteLine(role.Count);
            return role;
        }
        public string GetRoleBasedOnIdUserja(string user)
        {
            string sql = "SELECT * " +
                    "FROM AspNetUserRoles " +
                    "INNER JOIN " + "AspNetUsers ON (AspNetUserRoles.UserId = AspNetUsers.Id) " +
                    "INNER JOIN " + "AspNetRoles ON (AspNetUserRoles.RoleId = AspNetRoles.Id) "+
                    "WHERE (AspNetUserRoles.UserId = @Id)";
            var conn = GetConnection();

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", user);
            var reader = cmd.ExecuteReader();
           
            List<AspNetUserRoles> role = new List<AspNetUserRoles>();
            while (reader.Read())
            {
                role.Add(new AspNetUserRoles(reader, null));
            }
            // Console.WriteLine(role.Count);
            //  PropertyInfo inf = role[0].GetType().GetProperty("Name");
            //   return (string)(inf.GetValue(role[0], null));
            return role[0].RoleUserja.ToString();
        }
        public void InsertRolesOnUser(string user, string role)
        {
            string sql = "INSERT INTO AspNetUserRoles (UserId,RoleId) " +
                "values (@UserId, @RoleId)";
            var con = GetConnection();
            int rola;
            switch (role)
            {
                case "admin":
                    rola = 1;
                    break;
                case "Admin":
                    rola = 1;
                    break;
                case "ADMIN":
                    rola = 1;
                    break;

                case "user":
                    rola = 2;
                    break;
                case "User":
                    rola = 2;
                    break;
                case "USER":
                    rola = 2;
                    break;

                default:
                    rola = 2;
                    break;
            }

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@UserId", user);
            cmd.Parameters.AddWithValue("@RoleId", rola);
            cmd.ExecuteNonQuery();
        }
        public List<AspNetRoles> GetRoles()
        {
            string sql = "SELECT * FROM AspNetRoles";
            var con = GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            var reader = cmd.ExecuteReader();
            List<AspNetRoles> role = new List<AspNetRoles>();
            while (reader.Read())
            {
                role.Add(new AspNetRoles(reader));
            }
            return role;
        }
        public void PosodobiPravice(string user, int pravice)
        {
            string sql = "UPDATE AspNetUserRoles SET RoleId = @RoleId WHERE UserId=@UserId";
            var con = GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@UserId", user);
            cmd.Parameters.AddWithValue("@RoleId", pravice);
            cmd.ExecuteNonQuery();
        }
        public void InserNovoGlasovanje(string imeGlasovanja, DateTime zacetek, DateTime konec, string upodabnik,int vprasanje)
        {
            string sql = "INSERT INTO Glasovanja (GlasovanjeID ,ImeGlasovanja , GlasovanjeSeZacne ,GlasovanjeSeKonca, UporabnikiID , VprasanjeID) " +
                "values(@GlasovanjeID ,@ImeGlasovanja , @GlasovanjeSeZacne ,@GlasovanjeSeKonca, @UporabnikiID , @VprasanjeID)";
            var con = GetConnection();
            var cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@ImeGlasovanja",imeGlasovanja);
            cmd.Parameters.AddWithValue("@GlasovanjeSeZacne", zacetek);
            cmd.Parameters.AddWithValue("@GlasovanjeSeKonca", konec);
            cmd.Parameters.AddWithValue("@UporabnikiID", upodabnik);
            cmd.Parameters.AddWithValue("@VprasanjeID", vprasanje);
            cmd.ExecuteNonQuery();
        }
        public DataTable getDTable()
        {
            IzpisVPdf pd = new IzpisVPdf(_context,this);
            string sql = "SELECT  SeznamVprasanj.VprasanjeIzSeznama ,OddaniOdgovori.OddaniOdgovor , PravilniOdgovori.PravilniOdgovor ,OddaniOdgovori.UporabnikID , Odgovori.OdgovoriID " +
                "FROM Odgovori " +
                " INNER JOIN " + "SeznamVprasanj   ON (Odgovori.VprasanjeIzSeznamaID = SeznamVprasanj.SeznamVprasanjID) " +
                " INNER JOIN " + "OddaniOdgovori   ON (Odgovori.OddaniOdgovoriID     = OddaniOdgovori.OddaniOdgovoriID) " +
                " INNER JOIN " + "PravilniOdgovori ON (Odgovori.PravilniOdgovoriID   = PravilniOdgovori.PravilniOdgovoriID)";

            var con = GetConnection();
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
