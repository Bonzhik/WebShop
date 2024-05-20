using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Exceptions;
using WebShop.Services.Interfaces;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ILogger _logger;
        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }
        [HttpGet("{userId}")]
        [Authorize]
        public async Task<IActionResult> GetByUser(string userId)
        {
            _logger.LogInformation($"Request. Path: {HttpContext.Request.Path}{HttpContext.Request.QueryString}");
            _logger.LogInformation("The user makes a request to view the shopping cart!");
            try
            {
                CartR cart = await _cartService.GetAsync(userId);
                return Ok(cart);
            }
            catch (NotFoundException ex)
            {
                _logger.LogInformation($"The user's request to receive the shopping cart was not successful! Error: {ex.Message}");
                return StatusCode(502, ex.Message);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(CartW cartW)
        {
            _logger.LogInformation($"Request. Path: {HttpContext.Request.Path}{HttpContext.Request.QueryString}");
            _logger.LogInformation("The user makes a request to update the shopping cart!");
            try
            {
                if (!await _cartService.UpdateAsync(cartW))
                    return StatusCode(500, "Internal Server Error");
                _logger.LogInformation("The user was unable to update the shopping cart!");
                return Ok("Success");
            }
            catch (NotEnoughProductException ex)
            {
                _logger.LogInformation($"The user makes a request to update the shopping cart! Error: {ex.Message}");
                return StatusCode(503, ex.Message);
            }
        }
        [HttpPost("clear/{userId}")]
        [Authorize]
        public async Task<IActionResult> Clear(string userId)
        {
            _logger.LogInformation($"Request. Path: {HttpContext.Request.Path}{HttpContext.Request.QueryString}");
            _logger.LogInformation("The user has sent a request to clear the shopping cart!");
            try
            {
                if (!await _cartService.ClearAsync(userId))
                    return StatusCode(500, "Internal Server Error");

                _logger.LogInformation("The user has successfully cleared the shopping cart!");
                return Ok("Success");
            }
            catch (NotFoundException ex)
            {
                _logger.LogInformation($"The user was unable to empty the shopping cart! Error: {ex.Message}");
                return StatusCode(502, ex.Message);
            }
        }
    }
}
