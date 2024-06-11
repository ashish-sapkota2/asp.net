using Microsoft.EntityFrameworkCore;
using WebApiCrud.Models.Entities;

namespace WebApiCrud.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }
        public DbSet<Employee>Employees { get; set; }
    }
}
