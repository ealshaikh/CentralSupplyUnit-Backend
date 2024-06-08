using CSU_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSU_Core.Repository
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRoles();

        Task CreateRole(Role role);

        Task UpdateRole(Role role);

        Task DeleteRole(int roleId);
    }
}
