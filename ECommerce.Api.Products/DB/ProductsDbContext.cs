using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.DB;

public class ProductsDbContext:DbContext
{
    public DbSet<Product> Products { get; set; }

    public ProductsDbContext(DbContextOptions options):base(options)
    {
        
    }
}