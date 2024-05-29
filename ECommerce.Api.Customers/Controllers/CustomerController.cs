using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerProvider _customerProvider;

        public CustomerController(ICustomerProvider customerProvider)
        {
            _customerProvider = customerProvider;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var result = await _customerProvider.GetCustomersAsync();

            if (result.IsSuccess)
            {
                return Ok(result.orders);
            }

            return NotFound(result.ErrorMessage);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderAsync(int id)
        {
            var result = await _customerProvider.GetCustomerAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.order);
            }

            return NotFound(result.ErrorMessage);
        }
    }
}
