namespace ECommerce.Api.Search.Interfaces;

public interface ICustomerService
{
    Task<(bool isSuccess,dynamic Customer,string ErrorMessage)>GetCustomersAsync(int customerId);
}