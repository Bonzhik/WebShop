using Microsoft.AspNetCore.Mvc;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Services.ImageService;
using WebShop.Services.Interfaces;
using WebShop.Services.PaginationService;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IImageService _imageService;
        private readonly IPaginationService<ProductR> _paginationService;
        public ProductsController
            (
            IProductService productService, 
            IImageService imageService,
            IPaginationService<ProductR> paginationService)
        {
            _productService = productService;
            _imageService = imageService;
            _paginationService = paginationService; 
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<ProductR> productDtos = await _productService.GetAllAsync();
            return Ok(productDtos);
        }
        [HttpGet("GetPagination")]
        public async Task<IActionResult> PaginationGet([FromQuery] int page, int pageSize, string orderBy)
        {
            List<ProductR> products = await _productService.GetAllAsync();
            PaginationResponse<ProductR> result = _paginationService.Paginate(products, page, pageSize);

            switch (orderBy)
            {
                case "priceAsc":
                    result.Data = result.Data.OrderBy(x => x.Price).ToList();
                    break;
                case "priceDesc":
                    result.Data = result.Data.OrderByDescending(x => x.Price).ToList();
                    break;
                case "ratingAsc":
                    result.Data = result.Data.OrderBy(x => x.Rating).ToList();
                    break;
                case "ratingDesc":
                    result.Data = result.Data.OrderByDescending(x => x.Rating).ToList();
                    break;
                case "":
                    break;
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductW productDto)
        {
            await _productService.AddAsync(productDto);
            return Ok();
        }
    }
}
