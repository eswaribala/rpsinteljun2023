using ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Contexts
{
    public class ProductContext:DbContext
    {
        public ProductContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            this.Database.EnsureCreated();
        }

        
        public DbSet<Product> Products { get; set; }

    }
}
