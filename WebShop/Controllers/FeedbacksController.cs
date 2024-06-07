using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Exceptions;
using WebShop.Models;
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
        private readonly ISortingService<FeedbackR> _sortingService;
        public FeedbacksController
            (
            IFeedbackService feedbackService,
            ISortingService<FeedbackR> sortingService
            )
        {
            _feedbackService = feedbackService;
            _sortingService = sortingService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1,
                                             int pageSize = 10,
                                             string? sortField = null,
                                             string? sortOrder = null)
        {
            PaginationResponse<FeedbackR> result = await _feedbackService.GetAllAsync(page, pageSize);

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
                PaginationResponse<FeedbackR> result = await _feedbackService.GetByProductAsync(productId, page, pageSize);

                if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
                    result.Data = _sortingService.Sort(result.Data, sortField, sortOrder);

                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                //log
                return StatusCode(404, ex.Message);
            }
        }
        [HttpGet("byUser/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetByUser(string userId,
                                                   [FromQuery] int page = 1,
                                                   int pageSize = 10,
                                                   string? sortField = null,
                                                   string? sortOrder = null)
        {
            try
            {
                PaginationResponse<FeedbackR> result = await _feedbackService.GetByUserAsync(userId, page, pageSize);

                if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
                    result.Data = _sortingService.Sort(result.Data, sortField, sortOrder);

                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                //log
                return StatusCode(404, ex.Message);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(FeedbackW feedbackDto)
        {
            try
            {
                if (!await _feedbackService.AddAsync(feedbackDto))
                    return StatusCode(500, "Internal Server Error");
                //log
                return Ok();
            }
            catch (NotFoundException ex) 
            {
                return NotFound(ex.Message);
            }
            catch (AlreadyExistsException ex)
            {
                //log
                return StatusCode(501, ex.Message);
            }
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(FeedbackW feedbackDto)
        {
            try
            {
                if (!await _feedbackService.UpdateAsync(feedbackDto))
                    return StatusCode(500, "Internal Server Error");
                //log
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpDelete("{feedbackId}")]
        [Authorize]
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
                return NotFound(ex.Message);
            }
        }
    }
}
