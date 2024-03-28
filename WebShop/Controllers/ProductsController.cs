using Microsoft.AspNetCore.Mvc;
using WebShop.Dtos.Read;
using WebShop.Services.Interfaces;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<ProductR> productDtos = await _productService.GetAllAsync();
            return Ok(productDtos);
        }
    }
}
