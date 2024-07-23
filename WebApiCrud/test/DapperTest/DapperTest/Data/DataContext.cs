using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperTest.Data
{
    public class DataContext
    {
        private readonly IConfiguration config;
        private readonly string connectionString;

        public DataContext(IConfiguration config)
        {
            this.config = config;
            this.connectionString = this.config.GetConnectionString("Connection");
        }
        public IDbConnection CreateConnection()=> new SqlConnection(connectionString);
    }
}
