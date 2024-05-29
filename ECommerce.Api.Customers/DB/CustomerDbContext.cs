using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.DB;

public class CustomerDbContext:DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public CustomerDbContext(DbContextOptions options):base(options)
    {
        
    }
    
}