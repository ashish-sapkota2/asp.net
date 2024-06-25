using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PractiseSet.Data;
using PractiseSet.Models;

namespace PractiseSet.Repository
{
    public class SQLEmployeeRepository : IEmployee
    {
        private readonly PractiseDbContext context;

        public SQLEmployeeRepository(PractiseDbContext context)
        {
            this.context = context;
        }
        public async Task<List<Employee>> GetAll()
        {
            return await context.Employees.ToListAsync(); 
        }

   
    }
}
