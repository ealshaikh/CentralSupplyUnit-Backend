using CSU_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSU_Core.Repository
{
    public interface I_LoginRepository
    {
       User UserLogin(User login);

    }
}
