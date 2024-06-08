namespace CSU_Infra.Repository
{
    using CSU_Core.Common;
    using CSU_Core.Models;
    using CSU_Core.Repository;
    using Dapper;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    public class LoginRepository : I_LoginRepository
    {
        private readonly IDbContext _dbContext;

        public LoginRepository(IDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Login UserLogin(Login login)
        {
            var p = new DynamicParameters();
            p.Add("p_email", login.Email, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            p.Add("p_password", login.Password, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            IEnumerable<Login> result = _dbContext.Connection.Query<Login>("login_package.user_login", p, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }
    }
}
