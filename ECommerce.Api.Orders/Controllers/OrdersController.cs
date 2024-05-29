using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider _ordersProvider;

        public OrdersController(IOrdersProvider ordersProvider)
        {
            _ordersProvider = ordersProvider;
        }
        // [HttpGet]
        // public async Task<IActionResult> GetOrdersAsync()
        // {
        //     var result = await _ordersProvider.GetOrdersAsync();
        //
        //     if (result.IsSuccess)
        //     {
        //         return Ok(result.orders);
        //     }
        //
        //     return NotFound(result.ErrorMessage);
        // }
        
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrderAsync(int customerId)
        {
            var result = await _ordersProvider.GetOrdersAsync(customerId);

            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            }

            return NotFound(result.ErrorMessage);
        }
    }
}
