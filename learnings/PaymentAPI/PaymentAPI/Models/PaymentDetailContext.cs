using Microsoft.EntityFrameworkCore;

namespace PaymentAPI.Models
{
    public class PaymentDetailContext: DbContext
    {
        public PaymentDetailContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<PaymentDetails>PaymentDetails { get; set; }
    }
}
