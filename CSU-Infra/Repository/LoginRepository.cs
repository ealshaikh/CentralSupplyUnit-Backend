using CSU_Core.Models;
using CSU_Core.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Extensions.Logging;
using CSU_Core.Common; // Add this namespace for logging

namespace CSU_Infra.Repository
{
    public class LoginRepository : I_LoginRepository
    {
        private readonly IDbContext _dbContext;
        private readonly ILogger<LoginRepository> _logger; // Inject ILogger

        public LoginRepository(IDbContext dbContext, ILogger<LoginRepository> logger) // Add ILogger parameter to constructor
        {
            this._dbContext = dbContext;
            this._logger = logger;
        }

        public User UserLogin(User login)
        {
            var p = new DynamicParameters();
            p.Add("p_email", login.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("p_password", login.Password, dbType: DbType.String, direction: ParameterDirection.Input);

            IEnumerable<User> result = _dbContext.Connection.Query<User>("login_package.user_login", p, commandType: CommandType.StoredProcedure);

            return result.FirstOrDefault();
       }
    }
}
