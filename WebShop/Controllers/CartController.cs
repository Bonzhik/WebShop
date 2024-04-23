using Microsoft.AspNetCore.Mvc;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Exceptions;
using WebShop.Models;
using WebShop.Services.Interfaces;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUser(string userId)
        {
            try
            {
                CartR cart = await _cartService.GetAsync(userId);
                return Ok(cart);
            } catch (NotFoundException ex)
            {
                //log
                return StatusCode(502, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(CartW cartW)
        {
            try
            {
                if (!await _cartService.UpdateAsync(cartW))
                    return StatusCode(500, "Internal Server Error");
                //log
                return Ok("Success");
            } catch (NotEnoughProductException ex)
            {
                //log
                return StatusCode(503, ex.Message);
            }
        }
        [HttpPost("clear/{userId}")]
        public async Task<IActionResult> Clear(string userId)
        {
            try
            {
                if (!await _cartService.ClearAsync(userId))
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
