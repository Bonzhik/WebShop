using Microsoft.AspNetCore.Mvc;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Services.ImageService;
using WebShop.Services.Interfaces;
using WebShop.Services.PaginationService;
using WebShop.Services.SortingService;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IImageService _imageService;
        private readonly IPaginationService<ProductR> _paginationService;
        private readonly ISortingService<ProductR> _sortingService;
        public ProductsController
            (
            IProductService productService, 
            IImageService imageService,
            IPaginationService<ProductR> paginationService,
            ISortingService<ProductR> sortingService)
        {
            _productService = productService;
            _imageService = imageService;
            _paginationService = paginationService; 
            _sortingService = sortingService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<ProductR> productDtos = await _productService.GetAllAsync();
            return Ok(productDtos);
        }
        [HttpGet("pagination")]
        public async Task<IActionResult> PaginationGet([FromQuery] int page, int pageSize, string? sortField, string? sortOrder)
        {
            List<ProductR> products = await _productService.GetAllAsync();
            PaginationResponse<ProductR> result = _paginationService.Paginate(products, page, pageSize);

            if (sortField != null && sortOrder != null) 
            {
                result.Data = _sortingService.Sort(result.Data, sortField, sortOrder);  
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
