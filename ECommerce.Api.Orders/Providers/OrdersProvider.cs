using AutoMapper;

using ECommerce.Api.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;
using ECommerce.Api.Orders.DB;
using ECommerce.Api.Orders.Models;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext _dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Orders.Any())
            {
                
                _dbContext.Orders.Add(new DB.Orders()
        {
            Id = 1,
            CustomerId = 1,
            DateOnly = new DateOnly(2024, 5, 27), // Assuming today's date
            CustomerName = "John Doe",
            PaymentMethod = "Credit Card",
            ShippingAddress = "123 Main St, City, Country",
            BillAmount = 1000.00m, // Assuming bill amount is $1000.00
            Items = new List<OrderItem>()
            {
                new OrderItem() { OrderId = 1, ProductId = 1, Quantity = 2, UnitPrice = 50.00m }, // Product 1
                new OrderItem() { OrderId = 1, ProductId = 2, Quantity = 3, UnitPrice = 20.00m }, // Product 2
                new OrderItem() { OrderId = 1, ProductId = 3, Quantity = 1, UnitPrice = 100.00m } // Product 3
            }
        });

        // Order 2
        _dbContext.Orders.Add(new DB.Orders()
        {
            Id = 2,
            CustomerId = 2,
            DateOnly = new DateOnly(2024, 5, 26), // Assuming date
            CustomerName = "Jane Smith",
            PaymentMethod = "PayPal",
            ShippingAddress = "456 Oak St, City, Country",
            BillAmount = 500.00m, // Assuming bill amount is $500.00
            Items = new List<OrderItem>()
            {
                new OrderItem() { OrderId = 2, ProductId = 1, Quantity = 1, UnitPrice = 200.00m }, // Product 4
                new OrderItem() { OrderId = 2, ProductId = 2, Quantity = 2, UnitPrice = 150.00m }, // Product 5
            }
        });

        // Order 3
        _dbContext.Orders.Add(new DB.Orders()
        {
            Id = 3,
            CustomerId = 3,
            DateOnly = new DateOnly(2024, 5, 25), // Assuming date
            CustomerName = "Alice Johnson",
            PaymentMethod = "Bank Transfer",
            ShippingAddress = "789 Pine St, City, Country",
            BillAmount = 750.00m, // Assuming bill amount is $750.00
            Items = new List<OrderItem>()
            {
                new OrderItem() { OrderId = 3, ProductId = 1, Quantity = 1, UnitPrice = 300.00m }, // Product 6
                new OrderItem() { OrderId = 3, ProductId = 2, Quantity = 2, UnitPrice = 225.00m }, // Product 7
            }
        });

        // Order 4
        _dbContext.Orders.Add(new DB.Orders()
        {
            Id = 4,
            CustomerId = 4,
            DateOnly = new DateOnly(2024, 5, 24), // Assuming date
            CustomerName = "Bob Johnson",
            PaymentMethod = "Credit Card",
            ShippingAddress = "987 Elm St, City, Country",
            BillAmount = 300.00m, // Assuming bill amount is $300.00
            Items = new List<OrderItem>()
            {
                new OrderItem() { OrderId = 4, ProductId = 2, Quantity = 1, UnitPrice = 100.00m }, // Product 8
                new OrderItem() { OrderId = 4, ProductId = 1, Quantity = 1, UnitPrice = 200.00m }, // Product 9
            }
        });

        // Order 5
        _dbContext.Orders.Add(new DB.Orders()
        {
            Id = 5,
            CustomerId = 5,
            DateOnly = new DateOnly(2024, 5, 23), // Assuming date
            CustomerName = "Emily Brown",
            PaymentMethod = "Cash on Delivery",
            ShippingAddress = "654 Maple St, City, Country",
            BillAmount = 450.00m, // Assuming bill amount is $450.00
            Items = new List<OrderItem>()
            {
                new OrderItem() { OrderId = 5, ProductId = 1, Quantity = 2, UnitPrice = 150.00m }, // Product 10
                new OrderItem() { OrderId = 5, ProductId = 2, Quantity = 1, UnitPrice = 300.00m }, // Product 11
            }
        });
            }

            _dbContext.SaveChanges();
        }

        public async Task<(bool IsSuccess, IEnumerable<OrderModel> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await _dbContext.Orders
                    .Where(o => o.CustomerId == customerId)
                    .Include(o => o.Items)
                    .ToListAsync();
                var test = await _dbContext.Orders.FirstOrDefaultAsync(dbContext=>dbContext.CustomerId==customerId);
                if (orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<DB.Orders>, 
                        IEnumerable<OrderModel>>(orders);
                    return (true, result, null);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }    
        }
    }
}