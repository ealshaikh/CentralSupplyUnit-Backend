namespace CSU_Infra.Service
{
    using CSU_Core.Models;
    using CSU_Core.Repository;
    using CSU_Core.Service;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task CreateRole(Role role)
        {
            await _roleRepository.CreateRole(role);
        }

        public async Task DeleteRole(int roleId)
        {
            await _roleRepository.DeleteRole(roleId);
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await _roleRepository.GetAllRoles();
        }

        public async Task UpdateRole(Role role)
        {
            await _roleRepository.UpdateRole(role);
        }
    }
}
