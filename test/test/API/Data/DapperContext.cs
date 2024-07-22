using Microsoft.Data.SqlClient;
using System.Data;

namespace Test.API.Data
{
    public class DapperContext
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public DapperContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionString = this.configuration.GetConnectionString("Connection");
        }
        public IDbConnection CreateConnection() => new SqlConnection(connectionString);
    }
}
