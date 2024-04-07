using Microsoft.AspNetCore.Mvc;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Services.Interfaces;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]   
        public async Task<IActionResult> GetAll() 
        {
            List<OrderR> orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }
        [HttpPost]
        public async Task<IActionResult> Add(OrderW orderDto)
        {
            await _orderService.AddAsync(orderDto);
            return Ok();
        }
    }
}
