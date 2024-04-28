using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Exceptions;
using WebShop.Services.Interfaces;
using WebShop.Services.PaginationService;
using WebShop.Services.SortingService;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
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
        public async Task<IActionResult> Get([FromQuery] int page = 1,
                                             int pageSize = 10,
                                             string? sortField = null,
                                             string? sortOrder = null)
        {
            List<FeedbackR> feedbacks = await _feedbackService.GetAllAsync();
            PaginationResponse<FeedbackR> result = _paginationService.Paginate(feedbacks, page, pageSize);

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
                result.Data = _sortingService.Sort(result.Data, sortField, sortOrder);

            return Ok(result);
        }
        [HttpGet("byProduct/{productId}")]
        public async Task<IActionResult> GetByProduct(int productId,
                                                      [FromQuery] int page = 1,
                                                      int pageSize = 10,
                                                      string? sortField = null,
                                                      string? sortOrder = null)
        {
            try
            {
                List<FeedbackR> feedbacks = await _feedbackService.GetByProductAsync(productId);
                PaginationResponse<FeedbackR> result = _paginationService.Paginate(feedbacks, page, pageSize);

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
        [HttpGet("byUser/{userId}")]
        public async Task<IActionResult> GetByUser(string userId,
                                                   [FromQuery] int page = 1,
                                                   int pageSize = 10,
                                                   string? sortField = null,
                                                   string? sortOrder = null)
        {
            try
            {
                List<FeedbackR> feedbacks = await _feedbackService.GetByUserAsync(userId);
                PaginationResponse<FeedbackR> result = _paginationService.Paginate(feedbacks, page, pageSize);

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
        [HttpPost]
        public async Task<IActionResult> Add(FeedbackW feedbackDto)
        {
            try
            {
                if (!await _feedbackService.AddAsync(feedbackDto))
                    return StatusCode(500, "Internal Server Error");
                //log
                return Ok();
            }
            catch (AlreadyExistsException ex)
            {
                //log
                return StatusCode(501, ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(FeedbackW feedbackDto)
        {
            if (!await _feedbackService.UpdateAsync(feedbackDto))
                return StatusCode(500, "Internal Server Error");
            //log
            return Ok();
        }
        [HttpDelete("{feedbackId}")]
        public async Task<IActionResult> Delete(int feedbackId)
        {
            try
            {
                if (!await _feedbackService.DeleteAsync(feedbackId))
                    return StatusCode(500, "Internal Server Error");
                //log
                return Ok();
            }
            catch (NotFoundException ex)
            {
                //log
                return StatusCode(501, ex.Message);
            }
        }
    }
}
