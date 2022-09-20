using Gama_API.Model;
using Microsoft.EntityFrameworkCore;

namespace Gama_API.Data
{
    public class ShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ShopDbContext(DbContextOptions<ShopDbContext> options) 
            : base(options) => Database.EnsureCreated();

    }
}
