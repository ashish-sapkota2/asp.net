using Flight.Entity;
using Microsoft.EntityFrameworkCore;

namespace Flight.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DbContext> options): base(options) { 
        
        }
        public DbSet<AppUser>Users { get; set; }
    }
}
