using System;
using System.Collections.Generic;

namespace CSU_Core.Models
{
    public partial class User
    {
        public User()
        {
            Logins = new HashSet<Login>();
            Supplydocuments = new HashSet<Supplydocument>();
            Warehouses = new HashSet<Warehouse>();
        }

        public decimal Userid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? Username { get; set; }
        public string Password { get; set; } 
        public string Email { get; set; } 
        public decimal Roleid { get; set; }
        public string? RoleName { get; set; }


        public virtual Role? Role { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<Supplydocument> Supplydocuments { get; set; }
        public virtual ICollection<Warehouse> Warehouses { get; set; }
    }
}
