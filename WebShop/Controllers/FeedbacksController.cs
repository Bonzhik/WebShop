using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Net.Http.Headers;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Services.Implementations;
using WebShop.Services.Interfaces;
using WebShop.Services.PaginationService;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IPaginationService<FeedbackR> _paginationService;
        public FeedbacksController
            (
            IFeedbackService feedbackService,
            IPaginationService<FeedbackR> paginationService
            )
        {
            _feedbackService = feedbackService;
            _paginationService = paginationService; 
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page, int pageSize, string orderBy)
        {
            List<FeedbackR> feedbacks = await _feedbackService.GetAllAsync();
            PaginationResponse<FeedbackR> result = _paginationService.Paginate(feedbacks, page, pageSize);

            switch (orderBy)
            {
                case "ratingAsc":
                    result.Data = result.Data.OrderBy(x => x.Rating).ToList();
                    break;
                case "ratingDesc":
                    result.Data = result.Data.OrderByDescending(x => x.Rating).ToList();
                    break;
                case "":
                    break;
            }
            return Ok(result);
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
