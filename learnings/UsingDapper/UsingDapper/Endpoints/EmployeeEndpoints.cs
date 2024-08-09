using Dapper;
using Microsoft.Data.SqlClient;
using UsingDapper.Models;
using UsingDapper.Services;

namespace UsingDapper.Endpoints
{
    public static class EmployeeEndpoints
    {
        public static void MapEmployeeEndpoints( IEndpointRouteBuilder builder)
        {
            builder.MapGet("Employee", async (SqlConnectionFactory sqlConnection) =>
            {
                using var connection = sqlConnection.Create();
                const string sql = "SELECT * FROM Employees";
                var employee = await connection.Query<Employee>(sql);
                return Results.Ok(employee);

            });
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
