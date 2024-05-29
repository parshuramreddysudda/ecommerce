using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.DB;

public class OrdersDbContext:DbContext
{
    public DbSet<Orders> Orders { get; set; }
    public OrdersDbContext(DbContextOptions options):base(options)
    {
        
    }
    
}