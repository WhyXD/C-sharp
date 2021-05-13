using System;
using System.Collections.Generic;
using System.Data;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace AplikacijaGlasovanje.ModelsUpdate
{
    public partial class AspNetRoles
    {
        public AspNetRoles()
        {
            AspNetRoleClaims = new HashSet<AspNetRoleClaims>();
        }
        public AspNetRoles(IDataReader reader)
        {
            Id = (int)reader["Id"];
            Name = (string)reader["Name"];
            NormalizedName = (string)reader["NormalizedName"];
            ConcurrencyStamp = (string)reader["ConcurrencyStamp"];
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }

        public virtual ICollection<AspNetRoleClaims> AspNetRoleClaims { get; set; }
    }
}
