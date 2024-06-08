namespace CSU_Infra.Repository
{
    using CSU_Core.Common;
    using CSU_Core.Models;
    using CSU_Core.Repository;
    using Dapper;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public class RoleRepository : IRoleRepository
    {
        private readonly IDbContext _dbContext;

        public RoleRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateRole(Role role)
        {
            var p = new DynamicParameters();
            p.Add("p_name", role.Name, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("role_package.create_role", p, commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteRole(int roleId)
        {
            var p = new DynamicParameters();
            p.Add("p_roleid", roleId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("role_package.delete_role", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            var items = await _dbContext.Connection.QueryAsync<Role>("role_package.getallroles", commandType: CommandType.StoredProcedure);
            return items;
        }

        public async Task UpdateRole(Role role)
        {
            var p = new DynamicParameters();
            p.Add("p_roleid", role.Roleid, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            p.Add("p_name", role.Name, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("role_package.update_role", p, commandType: CommandType.StoredProcedure);
        }
    }
}
