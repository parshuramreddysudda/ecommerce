using AutoMapper;
using ECommerce.Api.Products.DB;
using ECommerce.Api.Products.Models.Profiles;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Tests;

public class ProductServiceTest
{
    [Fact]
    public  async Task GetProductsReturnsAllProducts()
    {
        var options = new DbContextOptionsBuilder<ProductsDbContext>()
            .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts)).Options;
        var dbContext = new ProductsDbContext(options);
        CreateProducts(dbContext);
        var productProfile = new ProductProfile();
        var configuration = new MapperConfiguration(configure =>
        {
            configure.AddProfile(productProfile);
        });
        var mapper = new Mapper(configuration);
        var productProvider = new ProductsProvider(dbContext,null,mapper);
        var result = await productProvider.GetProductsAsync();
        Assert.True(result.IsSuccess);
        Assert.True(result.products.Any());
        Assert.Null(result.ErrorMessage);
    }

    private void CreateProducts(ProductsDbContext dbContext)
    {
        for (int i = 1; i < 10; i++)
        {
            dbContext.Products.Add(new Product()
            {
                Id = i,
                Name = Guid.NewGuid().ToString(),
                Inventory = i * 10,
                Price = (decimal)(i * 35.21)
            });
        }

        dbContext.SaveChanges();
    }
    
    [Fact]
    public  async Task GetProductsReturnsProductUsingValidID()
    {
        var options = new DbContextOptionsBuilder<ProductsDbContext>()
            .UseInMemoryDatabase(nameof(GetProductsReturnsProductUsingValidID)).Options;
        var dbContext = new ProductsDbContext(options);
        CreateProducts(dbContext);
        var productProfile = new ProductProfile();
        var configuration = new MapperConfiguration(configure =>
        {
            configure.AddProfile(productProfile);
        });
        var mapper = new Mapper(configuration);
        var productProvider = new ProductsProvider(dbContext,null,mapper);
        var result = await productProvider.GetProductAsync(1);
        Assert.True(result.IsSuccess);
        Assert.True(result.product.Id==1);
        Assert.Null(result.ErrorMessage);
    }
    
    [Fact]
    public  async Task GetProductsReturnsProductUsingInValidID()
    {
        var options = new DbContextOptionsBuilder<ProductsDbContext>()
            .UseInMemoryDatabase(nameof(GetProductsReturnsProductUsingInValidID)).Options;
        var dbContext = new ProductsDbContext(options);
        CreateProducts(dbContext);
        var productProfile = new ProductProfile();
        var configuration = new MapperConfiguration(configure =>
        {
            configure.AddProfile(productProfile);
        });
        var mapper = new Mapper(configuration);
        var productProvider = new ProductsProvider(dbContext,null,mapper);
        var result = await productProvider.GetProductAsync(-1);
        Assert.False(result.IsSuccess);
        Assert.Null(result.product);
        Assert.NotNull(result.ErrorMessage);
    }
}