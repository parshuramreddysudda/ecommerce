using ECommerce.Api.Customers.Models;

namespace ECommerce.Api.Customers.Interfaces;

public interface ICustomerProvider
{
    Task<(bool IsSuccess, IEnumerable<CustomerModel> orders, string ErrorMessage)> GetCustomersAsync();
    Task<(bool IsSuccess, CustomerModel order, string ErrorMessage)> GetCustomerAsync(int id);
}