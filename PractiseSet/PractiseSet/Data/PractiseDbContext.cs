using Microsoft.EntityFrameworkCore;
using PractiseSet.Models;

namespace PractiseSet.Data
{
    public class PractiseDbContext : DbContext
    {
        public PractiseDbContext(DbContextOptions<PractiseDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }
        public DbSet<Employee>Employees { get; set; }
    }
}
