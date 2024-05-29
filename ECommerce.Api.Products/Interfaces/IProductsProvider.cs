using ECommerce.Api.Products.DB;
using ECommerce.Api.Products.Models;

namespace ECommerce.Api.Products.Interfaces;

public interface IProductsProvider
{
    Task<(bool IsSuccess, IEnumerable<ProductModel> products, string ErrorMessage)> GetProductsAsync();
    Task<(bool IsSuccess, ProductModel product, string ErrorMessage)> GetProductAsync(int id);
    
}