using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Products.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsProvider _provider;

        public ProductController(IProductsProvider provider)
        {
            _provider = provider;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var result = await _provider.GetProductsAsync();

            if (result.IsSuccess)
            {
                return Ok(result.products);
            }

            return NotFound(result.ErrorMessage);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await _provider.GetProductAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.product);
            }

            return NotFound(result.ErrorMessage);
        }
    }
}
