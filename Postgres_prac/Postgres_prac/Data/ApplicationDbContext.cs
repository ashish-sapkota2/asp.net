using Microsoft.EntityFrameworkCore;
using Postgres_prac.Models;

namespace Postgres_prac.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        
        }

        public DbSet<Student> students { get; set; }
    }
}
