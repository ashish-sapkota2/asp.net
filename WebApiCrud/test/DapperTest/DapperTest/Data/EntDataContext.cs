using DapperTest.Models;
using Microsoft.EntityFrameworkCore;

namespace DapperTest.Data
{
    public class EntDataContext :DbContext
    {
        public EntDataContext(DbContextOptions options) : base(options)
        {
            
        }
    public DbSet<ToDo>toDo {  get; set; }
    }
    
}
