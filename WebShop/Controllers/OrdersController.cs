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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger _logger;
        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }
        [HttpGet("{orderId}")]
        [Authorize]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            List<OrderR> orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }


        [HttpGet("byUser/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetByUser(string userId)
        {
            _logger.LogInformation($"Request. Path: {HttpContext.Request.Path}{HttpContext.Request.QueryString}");
            _logger.LogInformation("The user made a request to view the orders!");
            try
            {
                var orders = await _orderService.GetByUserAsync(userId);
                _logger.LogInformation("The user has successfully received the list of orders!");
                return Ok(orders);
            }
            catch (NotFoundException ex)
            {
                _logger.LogInformation($"The user could not receive the list of orders! Error: {ex.Message}");
                return StatusCode(404, ex.Message);
            }
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(OrderW orderDto)
        {
            _logger.LogInformation($"Request. Path: {HttpContext.Request.Path}{HttpContext.Request.QueryString}");
            _logger.LogInformation("The user has made a request to create an order!");
            try
            {
                if (!await _orderService.AddAsync(orderDto))
                    return StatusCode(500, "Internal Server Error");

                _logger.LogInformation("The user has successfully created an order!");
                return Ok("Success");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (NotEnoughProductException ex)
            {
                _logger.LogInformation($"The user was unable to create an order! Error: {ex.Message}");
                return StatusCode(503, ex.Message);
            }
        }

        [HttpDelete("{orderId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int orderId)
        {
            _logger.LogInformation($"Request. Path: {HttpContext.Request.Path}{HttpContext.Request.QueryString}");
            _logger.LogInformation("The user has made a request to delete the order!");
            try
            {
                if (!await _orderService.DeleteAsync(orderId))
                    return StatusCode(500, "Internal Server Error");

                _logger.LogInformation("The user has successfully deleted the order!");
                return Ok("Success");
            }
            catch (NotFoundException ex)
            {
                _logger.LogInformation($"The user was unable to delete the order! Error: {ex.Message}");
                return StatusCode(404, ex.Message);
            }
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromQuery] int orderId, int statusId)
        {
            try
            {
                if (!await _orderService.UpdateAsync(orderId, statusId))
                    return StatusCode(500, "Internal Server Error");
                //log
                return Ok("Success");
            } catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
