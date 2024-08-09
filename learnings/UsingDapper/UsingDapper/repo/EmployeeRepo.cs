
using Dapper;
using Microsoft.IdentityModel.Tokens;
using UsingDapper.Models.Data;

namespace UsingDapper.repo
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly DapperDbContext context;

        public EmployeeRepo(DapperDbContext context)
        {
            this.context = context;
        }
        public async Task<List<EmployeeRepo>> GetAll()
        {
            string query = "Select * from tbl_employee";
            using (var connection = this.context.CreateConnection()) {
                var emplist = await  connection.QueryAsync<EmployeeRepo>(query);
                return emplist.ToList();
            }
        }
    }
}
