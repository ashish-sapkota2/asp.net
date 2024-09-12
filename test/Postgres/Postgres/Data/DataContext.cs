using Microsoft.EntityFrameworkCore;
using Postgres.Models;

namespace Postgres.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions <DataContext>options): base(options) 
        {
        
        }
        public DbSet<Order>orders { get; set; }
    }
}
