using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Postgres.Models
{
    [Table("product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Brand { get; set; }= string.Empty;
        public int Size { get; set; } 
        public int price {  get; set; }
    }
}
