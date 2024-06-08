using System;
using System.Collections.Generic;

namespace CSU_Core.Models
{
    public partial class Login
    {
        public decimal Loginid { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Roleid { get; set; }
        public decimal Userid { get; set; }

        public virtual Role? Role { get; set; }
        public virtual User? User { get; set; }
    }
}
