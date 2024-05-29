using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Services;

public class SearchService:ISearchService
{
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    private readonly ICustomerService _customerService;

    public SearchService(IOrderService orderService,IProductService productService,ICustomerService customerService)
    {
        _customerService = customerService;
        _productService = productService;
        _orderService = orderService;
    }
    public async Task<(bool isSuccess, dynamic SearchResults)> SearchAsync(int customerId)
    {
        var orderResult = await _orderService.GetOrdersAsync(customerId);
        var customerResult = await _customerService.GetCustomersAsync(customerId);
        var productsResult = await _productService.GetProductAsync();
        if (orderResult.isSuccess)
        {
       
            foreach(var order in orderResult.Item2){
                foreach (var item in order.Items)
                {
                    item.ProductName = productsResult.IsSuccess? productsResult.Item2.FirstOrDefault(p => p.Id == item.ProductId)?.Name:"Product Information is not avaialble";
                }
            }
            var result = new
            {
                Customer = customerResult.isSuccess?
                    customerResult.Customer:new{Name="No Customer Available"},
                Order = orderResult.Item2
            };
            return (true, result);
        }

        return (false,"Not Found");
    }
}