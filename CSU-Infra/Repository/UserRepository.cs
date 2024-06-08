namespace CSU_Infra.Repository
{
    using CSU_Core.Common;
    using CSU_Core.Models;
    using CSU_Core.Repository;
    using Dapper;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public class UserRepository : IUserRepository
    {
        private readonly IDbContext _dbContext;

        public UserRepository(IDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task CreateUser(User user)
        {
            var p = new DynamicParameters();
            p.Add("p_firstname", user.Firstname, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            p.Add("p_lastname", user.Lastname, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            p.Add("p_password", user.Password, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            p.Add("p_email", user.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_roleid", user.Roleid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("user_package.create_user", p, commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteUser(int userId)
        {
            var p = new DynamicParameters();
            p.Add("p_userid", userId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("user_package.delete_user", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var sql = @"
                 SELECT u.*, r.Name AS RoleName
                  FROM users u
                   
                JOIN role r ON u.Roleid = r.Roleid";

            var users = await _dbContext.Connection.QueryAsync<User, string, User>(
                sql,
                (user, roleName) =>
                {
                    user.RoleName = roleName;
                    return user;
                },
                splitOn: "RoleName"
            );

            return users;
        }

        public async Task<string> GetRoleName(int roleId)
        {
            var p = new DynamicParameters();
            p.Add("p_role_id", roleId, dbType: System.Data.DbType.Int32, ParameterDirection.Input);
            p.Add("p_role_name", dbType: System.Data.DbType.String, direction: ParameterDirection.Output, size: 255);

            await _dbContext.Connection.ExecuteAsync("user_package.get_role_name", p, commandType: CommandType.StoredProcedure);

            return p.Get<string>("p_role_name");
        }

        public async Task UpdateUser(User user)
        {
            var p = new DynamicParameters();
            p.Add("p_userid", user.Userid, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Input);
            p.Add("p_firstname", user.Firstname, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            p.Add("p_lastname", user.Lastname, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            p.Add("p_password", user.Password, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            p.Add("p_email", user.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_roleid", user.Roleid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("user_package.update_user", p, commandType: CommandType.StoredProcedure);
        }
    }
}
