using System.Text.Json;
using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Services;

public class ProductService:IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<ISearchService> _logger;

    public ProductService(IHttpClientFactory httpClientFactory,ILogger<ISearchService> logger)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }
    public async Task<(bool IsSuccess, IEnumerable<Products>, string ErrorMessage)> GetProductAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("ProductsService");
            var response = await client.GetAsync($"api/Product/GetProducts");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsByteArrayAsync();
                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<IEnumerable<Products>>(content,options);
                return (true, result, null);

            }
            return (false, null, "Not Found");

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _logger?.LogError(e.ToString());
            return (false, null, e.Message);
           
        }
    }
}