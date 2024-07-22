using Microsoft.EntityFrameworkCore;
using PractiseSet.Data;
using PractiseSet.Models;

namespace PractiseSet.Repositories
{
    public class SQLEmployeeRepository: IEmployeeRepository
    {
        private readonly EmployeeDbContext dbContext;

        public SQLEmployeeRepository(EmployeeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await dbContext.Employees.ToListAsync();
        }
    }

}
