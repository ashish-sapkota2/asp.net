using DatingApp.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace DatingApp.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<AppUser>Users { get; set; }

    }
}
