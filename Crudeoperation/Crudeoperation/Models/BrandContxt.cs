using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;

namespace Crudeoperation.Models
{
    public class BrandContxt: DbContext 
    {
        public BrandContxt(DbContextOptions<BrandContxt>options): base(options) { 
        
        }
        public DbSet<Brand>Brands { get; set; }
    }
}
