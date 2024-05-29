using ECommerce.Api.Orders.Models;
using ECommerce.Api.Orders.Models.Profiles;

namespace ECommerce.Api.Orders.Interfaces;

public interface IOrdersProvider
{
    //Task<(bool IsSuccess, IEnumerable<OrderModel> orders, string ErrorMessage)> GetOrdersAsync();
    Task<(bool IsSuccess, IEnumerable<OrderModel> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
}