using Microsoft.EntityFrameworkCore;
using PractiseSet.Models;

namespace PractiseSet.Data
{
    public class EmployeeDbContext: DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> dbContext): base(dbContext)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
