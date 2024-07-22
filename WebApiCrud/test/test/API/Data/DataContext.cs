using Microsoft.EntityFrameworkCore;
using Test.API.Models;

namespace Test.API.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions options): base(options)
        {
            
        }
        public DbSet<AppUser>Users { get; set; }
    }
}
