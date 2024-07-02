using Microsoft.Data.SqlClient;
using System.Data;

namespace LearningDapper.dapperContext
{
    public class DapperDbContext
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public DapperDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionString = this.configuration.GetConnectionString("Database");
        }
        public IDbConnection CreateConnection() => new SqlConnection(connectionString); 
    }
}
