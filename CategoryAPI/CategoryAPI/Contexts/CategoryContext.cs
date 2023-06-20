using CategoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CategoryAPI.Contexts
{
    public class CategoryContext:DbContext
    {
        public CategoryContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<Category> Categories { get; set; }
        

    }
}
