using Microsoft.AspNetCore.Mvc;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Services.Implementations;
using WebShop.Services.Interfaces;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbacksController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<FeedbackR> feedbacks = await _feedbackService.GetAllAsync();
            return Ok(feedbacks);
        }
        [HttpGet("byProduct/{productId}")]
        public async Task<IActionResult> GetByProduct(int productId)
        {
            List<FeedbackR> productDtos = await _feedbackService.GetByProductAsync(productId);
            return Ok(productDtos);
        }
        [HttpPost]
        public async Task<IActionResult> Add(FeedbackW feedbackDto)
        {
            await _feedbackService.AddAsync(feedbackDto);   
            return Ok();
        }
    }
}
