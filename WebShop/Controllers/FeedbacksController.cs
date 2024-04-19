using Microsoft.AspNetCore.Mvc;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Services.Interfaces;
using WebShop.Services.PaginationService;
using WebShop.Services.SortingService;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IPaginationService<FeedbackR> _paginationService;
        private readonly ISortingService<FeedbackR> _sortingService;
        public FeedbacksController
            (
            IFeedbackService feedbackService,
            IPaginationService<FeedbackR> paginationService,
            ISortingService<FeedbackR> sortingService
            )
        {
            _feedbackService = feedbackService;
            _paginationService = paginationService; 
            _sortingService = sortingService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page, int pageSize, string? sortField, string? sortOrder)
        {
            List<FeedbackR> feedbacks = await _feedbackService.GetAllAsync();
            PaginationResponse<FeedbackR> result = _paginationService.Paginate(feedbacks, page, pageSize);

            if (sortField != null && sortOrder != null)
            {
                result.Data = _sortingService.Sort(result.Data, sortField, sortOrder);
            }

            return Ok(result);
        }
        [HttpGet("byProduct/{productId}")]
        public async Task<IActionResult> GetByProduct(int productId, [FromQuery] int page, int pageSize, string? sortField, string? sortOrder)
        {
            List<FeedbackR> feedbacks = await _feedbackService.GetByProductAsync(productId);
            PaginationResponse<FeedbackR> result = _paginationService.Paginate(feedbacks, page, pageSize);

            if (sortField != null && sortOrder != null)
            {
                result.Data = _sortingService.Sort(result.Data, sortField, sortOrder);
            }

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add(FeedbackW feedbackDto)
        {
            await _feedbackService.AddAsync(feedbackDto);   
            return Ok();
        }
    }
}
