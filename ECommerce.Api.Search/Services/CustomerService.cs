using System.Text.Json;
using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Services;

public class CustomerService:ICustomerService
{
    private readonly ILogger<CustomerService> _iLogger;
    private readonly IHttpClientFactory _clientFactory;

    public CustomerService(IHttpClientFactory clientFactory,ILogger<CustomerService> iLogger)
    {
        _clientFactory = clientFactory;
        _iLogger = iLogger;
    }
    public async Task<(bool isSuccess, dynamic Customer, string ErrorMessage)> GetCustomersAsync(int customerId)
    {
        try
        {
            var client = _clientFactory.CreateClient("CustomerService");
            var response = await client.GetAsync($"api/Customer/{customerId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsByteArrayAsync();
                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<Customer>(content, options);
                return (true, result, "");
            }
            return (false, null, "");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _iLogger?.LogError(e.ToString());
            return (false, null, e.Message);
            throw;
        }
    }
}