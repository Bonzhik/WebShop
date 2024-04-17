using Microsoft.AspNetCore.Mvc;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
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
            CartR cart = await _cartService.GetAsync(userId);
            return Ok(cart);
        }
        [HttpPost]
        public async Task<IActionResult> Add(CartW cartW) { 
            await _cartService.UpdateAsync(cartW);
            return Ok();
        }
    }
}
