using Microsoft.AspNetCore.Mvc;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Services.ImageService;
using WebShop.Services.Interfaces;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IImageService _imageService;
        public ProductsController(IProductService productService, IImageService imageService)
        {
            _productService = productService;
            _imageService = imageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<ProductR> productDtos = await _productService.GetAllAsync();
            return Ok(productDtos);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductW productDto)
        {
            await _productService.AddAsync(productDto);
            return Ok();
        }
    }
}
