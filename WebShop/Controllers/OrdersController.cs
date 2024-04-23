using Microsoft.AspNetCore.Mvc;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Exceptions;
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
        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get(int orderId)
        {
            var order = await _orderService.GetAsync(orderId);  
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<OrderR> orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }
        [HttpGet("byUser/{userId}")]
        public async Task<IActionResult> GetByUser(string userId)
        {
            try
            {
                var orders = await _orderService.GetByUserAsync(userId);
                return Ok(orders);
            }
            catch (NotFoundException ex)
            {
                //log
                return StatusCode(502, ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(OrderW orderDto)
        {
            try
            {
                if (!await _orderService.AddAsync(orderDto))
                    return StatusCode(500, "Internal Server Error");

                //log
                return Ok("Success");
            }
            catch (NotEnoughProductException ex)
            {
                //log
                return StatusCode(503, ex.Message);
            }
        }
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> Delete(int orderId)
        {
            try
            {
                if (!await _orderService.DeleteAsync(orderId))
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
