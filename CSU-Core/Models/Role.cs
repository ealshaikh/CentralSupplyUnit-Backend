using System;
using System.Collections.Generic;

namespace CSU_Core.Models
{
    public partial class Role
    {
        public Role()
        {
            Logins = new HashSet<Login>();
            Users = new HashSet<User>();
        }

        public decimal Roleid { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Login> Logins { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
