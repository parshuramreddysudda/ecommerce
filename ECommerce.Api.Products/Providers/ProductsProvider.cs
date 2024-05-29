using AutoMapper;
using ECommerce.Api.Products.DB;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Providers;

public class ProductsProvider:IProductsProvider
{
    private readonly ProductsDbContext _dbContext;
    private readonly ILogger<ProductsProvider> _logger;
    private readonly IMapper _mapper;

    public ProductsProvider(ProductsDbContext dbContext,ILogger<ProductsProvider> logger,IMapper mapper)
    {
        _mapper = mapper;
        _logger = logger;
        _dbContext = dbContext;
        SeedData();
    }

    private  void SeedData()
    {
       
        if (!_dbContext.Products.Any())
        {
            _dbContext.Products.Add(new Product() { Id = 1, Name = "Keyboard", Price = 100, Inventory = 2 });
            _dbContext.Products.Add(new Product() { Id = 2, Name = "Mouse", Price = 50, Inventory = 5 });
            _dbContext.Products.Add(new Product() { Id = 3, Name = "Monitor", Price = 200, Inventory = 3 });
            _dbContext.Products.Add(new Product() { Id = 4, Name = "Laptop", Price = 800, Inventory = 10 });
            _dbContext.Products.Add(new Product() { Id = 5, Name = "Headphones", Price = 50, Inventory = 7 });
            _dbContext.Products.Add(new Product() { Id = 6, Name = "Desk", Price = 150, Inventory = 1 });
            _dbContext.Products.Add(new Product() { Id = 7, Name = "Chair", Price = 80, Inventory = 4 });
            _dbContext.Products.Add(new Product() { Id = 8, Name = "Printer", Price = 200, Inventory = 2 });
            _dbContext.Products.Add(new Product() { Id = 9, Name = "Scanner", Price = 120, Inventory = 3 });
            _dbContext.Products.Add(new Product() { Id = 10, Name = "External Hard Drive", Price = 120, Inventory = 6 });
            _dbContext.SaveChanges();

        }
    }
    public async Task<(bool IsSuccess, IEnumerable<ProductModel> products, string ErrorMessage)> GetProductsAsync()
    {
        try
        {
            var products = await _dbContext.Products.ToListAsync();
            if (products != null && products.Any())
            {
                var results=_mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(products);
                return (true, results, null);
            }

            return (false, null, "Not Found");
        }
        catch (Exception e)
        {
            _logger?.LogError(e.ToString());
            return (false, null, e.Message);
        }
    }
    public async Task<(bool IsSuccess, ProductModel product, string ErrorMessage)> GetProductAsync(int id)
    {
        try
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(p=>p.Id==id);
            if (product != null)
            {
                var result=_mapper.Map<Product, ProductModel>(product);
                return (true, result, null);
            }

            return (false, null, "Not Found");
        }
        catch (Exception e)
        {
            _logger?.LogError(e.ToString());
            return (false, null, e.Message);
        }
    }
}