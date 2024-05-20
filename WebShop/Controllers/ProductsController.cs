﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core;
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
    [EnableCors]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IPaginationService<ProductR> _paginationService;
        private readonly ISortingService<ProductR> _sortingService;
        private readonly ILogger _logger;
        public ProductsController
            (
            IProductService productService,
            IImageService imageService,
            IPaginationService<ProductR> paginationService,
            ISortingService<ProductR> sortingService,
            ILogger<ProductsController> logger)
        {
            _productService = productService;
            _paginationService = paginationService;
            _sortingService = sortingService;
            _logger = logger;
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
        public async Task<IActionResult> GetAll([FromQuery] int page = 1,
                                                int pageSize = 10,
                                                string? sortField = null,
                                                string? sortOrder = null,
                                                int minPrice = 1,
                                                int maxPrice = 300000)
        {
            List<ProductR> products = await _productService.GetAllAsync();
            products = products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
            PaginationResponse<ProductR> result = _paginationService.Paginate(products, page, pageSize);

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
                result.Data = _sortingService.Sort(result.Data, sortField, sortOrder);

            return Ok(result);
        }
        [HttpGet("latest")]
        public async Task<IActionResult> GetLatest()
        {
            var products = await _productService.GetLatest();
            return Ok(products);
        }
        [HttpGet("byCategory")]
        public async Task<IActionResult> GetByCategory([FromQuery] int[] categoryId,
                                                       int page = 1,
                                                       int pageSize = 10,
                                                       string? sortField = null,
                                                       string? sortOrder = null,
                                                       int minPrice = 1,
                                                       int maxPrice = 300000)
        {
            if (categoryId == null || categoryId.Length == 0)
                return BadRequest();

            try
            {
                List<ProductR> products = await _productService.GetByCategoryAsync(categoryId);
                products = products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
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
        [HttpGet("bySubategory")]
        public async Task<IActionResult> GetBySubCategory([FromQuery] int[] subcategoryId,
                                                          int page = 1,
                                                          int pageSize = 10,
                                                          string? sortField = null,
                                                          string? sortOrder = null,
                                                          int minPrice = 1,
                                                          int maxPrice = 300000)
        {
            if (subcategoryId == null || subcategoryId.Length == 0)
                return BadRequest();

            try
            {
                List<ProductR> products = await _productService.GetBySubcategoryAsync(subcategoryId);
                products = products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
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
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string search,
                                                int page = 1,
                                                int pageSize = 10,
                                                string? sortField = null,
                                                string? sortOrder = null)
        {
            if (string.IsNullOrEmpty(search))
                return BadRequest("Пустая строка");

            try
            {
                List<ProductR> products = await _productService.Search(search);
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(ProductW productDto)
        {
            _logger.LogInformation($"Request. Path: {HttpContext.Request.Path}{HttpContext.Request.QueryString}");
            _logger.LogInformation("The user has made a request to create a product!");
            try
            {
                if (!await _productService.AddAsync(productDto))
                    return StatusCode(500, "Internal Server Error");
                _logger.LogInformation("The user has successfully added the product!");
                return Ok("Success");
            }
            catch (AlreadyExistsException ex)
            {
                _logger.LogInformation($"The user was unable to add the product! Error: {ex.Message}");
                return StatusCode(501, ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(ProductW productDto)
        {
            _logger.LogInformation($"Request. Path: {HttpContext.Request.Path}{HttpContext.Request.QueryString}");
            _logger.LogInformation("The user made a request to edit the product!");
            try
            {
                if (!await _productService.UpdateAsync(productDto))
                    return StatusCode(500, "Internal Server Error");
                _logger.LogInformation("The user has successfully updated the product!");
                return Ok("Success");
            }
            catch (AlreadyExistsException ex)
            {
                _logger.LogInformation($"The user was unable to update the product! Error: {ex.Message}");
                return StatusCode(501, ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int productId)
        {
            _logger.LogInformation($"Request. Path: {HttpContext.Request.Path}{HttpContext.Request.QueryString}");
            _logger.LogInformation("The user has made a request to delete the product!");
            try
            {
                if (!await _productService.DeleteAsync(productId))
                    return StatusCode(500, "Internal Server Error");
                _logger.LogInformation("\r\nThe user has successfully deleted the product!");
                return Ok("Success");
            }
            catch (NotFoundException ex)
            {
                _logger.LogInformation($"The user was unable to delete the product! {ex.Message}");
                return StatusCode(502, ex.Message);
            }
        }
    }
}
