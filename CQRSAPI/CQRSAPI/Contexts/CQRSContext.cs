using CQRSAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRSAPI.Contexts
{
    public class CQRSContext:DbContext
    {
        public CQRSContext(DbContextOptions<CQRSContext> options):base(options) { 
        
            this.Database.EnsureCreated();
            this.Database.Migrate();

        }

        public DbSet<Cart> Carts { get; set; }  
        public DbSet<Product> Products { get; set; }    
    }
}
