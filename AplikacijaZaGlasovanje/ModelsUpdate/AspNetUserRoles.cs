using System;
using System.Collections.Generic;
using System.Data;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AplikacijaGlasovanje.ModelsUpdate
{
    public partial class AspNetUserRoles
    {
        public AspNetUserRoles() { }
        public AspNetUserRoles(IDataReader reader)
        {
            UserId = (string)reader["UserId"];
            RoleId = (int)reader["RoleId"];
        }
        public AspNetUserRoles(IDataReader reader,string b)
        {
            IdUserjaIzDrugeTabele = (string)reader["Id"];
            RoleUserja = (string)reader["Name"];
        }
        public string UserId { get; set; }
        public int RoleId { get; set; }
        //
        public string IdUserjaIzDrugeTabele { get; set; }
        public string RoleUserja { get; set; }
        //

        public virtual AspNetRoles Role { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
