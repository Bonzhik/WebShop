﻿using Microsoft.AspNetCore.Mvc;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Exceptions;
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
        [HttpGet("{productId}")]
        public async Task<IActionResult> Get(int productId)
        {
            var product = await _productService.GetAsync(productId);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, int pageSize = 10, string? sortField = null, string? sortOrder = null)
        {
            List<ProductR> products = await _productService.GetAllAsync();
            PaginationResponse<ProductR> result = _paginationService.Paginate(products, page, pageSize);

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
                result.Data = _sortingService.Sort(result.Data, sortField, sortOrder);

            return Ok(result);
        }
        [HttpGet("byCategory/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId, [FromQuery] int page = 1, int pageSize = 10, string? sortField = null, string? sortOrder = null)
        {
            try
            {
                List<ProductR> products = await _productService.GetByCategoryAsync(categoryId);
                PaginationResponse<ProductR> result = _paginationService.Paginate(products, page, pageSize);

                if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
                    result.Data = _sortingService.Sort(result.Data, sortField, sortOrder);

                return Ok(result);

            }
            catch (NotFoundException ex)
            {
                //log
                return StatusCode(502, ex.Message);
            }
        }
        [HttpGet("bySubategory/{subcategoryId}")]
        public async Task<IActionResult> GetBySubCategory(int subcategoryId, [FromQuery] int page = 1, int pageSize = 10, string? sortField = null, string? sortOrder = null)
        {
            try
            {
                List<ProductR> products = await _productService.GetBySubcategoryAsync(subcategoryId);
                PaginationResponse<ProductR> result = _paginationService.Paginate(products, page, pageSize);

                if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
                    result.Data = _sortingService.Sort(result.Data, sortField, sortOrder);

                return Ok(result);

            }
            catch (NotFoundException ex)
            {
                //log
                return StatusCode(502, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductW productDto)
        {
            try
            {
                if (!await _productService.AddAsync(productDto))
                    return StatusCode(500, "Internal Server Error");
                //log
                return Ok("Success");
            }
            catch (AlreadyExistsException ex)
            {
                //log
                return StatusCode(501, ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(ProductW productDto)
        {
            try
            {
                if (!await _productService.UpdateAsync(productDto))
                    return StatusCode(500, "Internal Server Error");
                //log
                return Ok("Success");
            }
            catch (AlreadyExistsException ex)
            {
                //log
                return StatusCode(501, ex.Message);
            }
        }
        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            try
            {
                if (!await _productService.DeleteAsync(productId))
                    return StatusCode(500, "Internal Server Error");
                //log
                return Ok("Success");
            }
            catch (NotFoundException ex)
            {
                //log
                return StatusCode(502, ex.Message);
            }
        }
    }
}
