using DatingApp_Dapper.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp_Dapper.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {
        
        }
        public DbSet<AppUsers> Users { get; set; }
    }
}
