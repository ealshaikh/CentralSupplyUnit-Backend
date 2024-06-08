namespace CSU_Core.Service
{
    using CSU_Core.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRoles();

        Task CreateRole(Role role);

        Task UpdateRole(Role role);

        Task DeleteRole(int roleId);
    }
}
