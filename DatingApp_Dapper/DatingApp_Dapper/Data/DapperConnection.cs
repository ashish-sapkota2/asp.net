using Microsoft.Data.SqlClient;
using System.Data;

namespace DatingApp_Dapper.Data
{
    public class DapperConnection
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public DapperConnection(IConfiguration configuration) 
        {
            this.configuration = configuration;
            this.connectionString = this.configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(connectionString);
    }
}
