using System.Text.Json;
using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Services;

public class OrdersService:IOrderService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly ILogger _loggerFactory;

    public OrdersService(IHttpClientFactory clientFactory,ILogger<OrdersService> loggerFactory)
    {
        _loggerFactory = loggerFactory;
        _clientFactory = clientFactory;
    }
    public async Task<(bool isSuccess, IEnumerable<Order>, string ErrorMessage)> GetOrdersAsync(int customerId)
    {
        try
        {
            var client = _clientFactory.CreateClient("OrdersService");
            var response = await client.GetAsync($"api/Orders/{customerId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsByteArrayAsync();
                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content,options);
                return (true, result, null);

            }
            return (false, null, null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _loggerFactory?.LogError(e.ToString());
            return (false, null, e.Message);
            throw;
        }
    }
}