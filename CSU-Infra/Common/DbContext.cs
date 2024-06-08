using CSU_Core.Common;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Data.Common;

namespace CSU_Infra.Common
{
    public class DbContext : IDbContext
    {
        private readonly IConfiguration _configuration;

        private DbConnection _connection;

        public DbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new OracleConnection(_configuration["ConnectionStrings:DBConnectionString"]);
                    _connection.Open();
                }
                else if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();

                }
                return _connection;
            }
        }
    }
}