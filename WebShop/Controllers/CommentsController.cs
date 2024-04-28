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
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IPaginationService<CommentR> _paginationsService;
        private readonly ISortingService<CommentR> _sortingService;
        public CommentsController
            (
            ICommentService commentService,
            IPaginationService<CommentR> paginationService,
            ISortingService<CommentR> sortingService
            )
        {
            _paginationsService = paginationService;
            _commentService = commentService;
            _sortingService = sortingService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, int pageSize = 10, string? sortField = null, string? sortOrder = null)
        {
            List<CommentR> comments = await _commentService.GetAllAsync();
            PaginationResponse<CommentR> result = _paginationsService.Paginate(comments, page, pageSize);

            if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
                result.Data = _sortingService.Sort(result.Data, sortField, sortOrder);

            return Ok(result);
        }
        [HttpGet("byFeedback/{feedbackId}")]
        public async Task<IActionResult> GetByFeedback(int feedbackId, [FromQuery] int page = 1, int pageSize = 10, string? sortField = null, string? sortOrder = null)
        {
            try
            {
                List<CommentR> comments = await _commentService.GetAllAsync();
                PaginationResponse<CommentR> result = _paginationsService.Paginate(comments, page, pageSize);

                if (!string.IsNullOrEmpty(sortField) && !string.IsNullOrEmpty(sortOrder))
                    result.Data = _sortingService.Sort(result.Data, sortField, sortOrder);

                return Ok(result);
            } catch (NotFoundException ex) 
            {
                //log
                return StatusCode(502, ex.Message); 
            }
        }
        [HttpGet("byComment/{commentId}")]
        public async Task<IActionResult> GetByComment(int commentId, [FromQuery] int page = 1, int pageSize = 10, string? sortField = null, string? sortOrder = null)
        {
            try
            {
                List<CommentR> comments = await _commentService.GetByParentCommentAsync(commentId);
                PaginationResponse<CommentR> result = _paginationsService.Paginate(comments, page, pageSize);

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
        public async Task<IActionResult> GetByComment(string userId, [FromQuery] int page = 1, int pageSize = 10, string? sortField = null, string? sortOrder = null)
        {
            try
            {
                List<CommentR> comments = await _commentService.GetByUserAsync(userId);
                PaginationResponse<CommentR> result = _paginationsService.Paginate(comments, page, pageSize);

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
        public async Task<IActionResult> Add(CommentW comment)
        {
            if (!await _commentService.AddAsync(comment))
                return StatusCode(500, "Internal Server Error");
            //log
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update(CommentW comment)
        {
            if (!await _commentService.UpdateAsync(comment))
                return StatusCode(500, "Internal Server Error");
            //log
            return Ok();
        }
        [HttpDelete("{commentId}")]
        public async Task<IActionResult> Delete(int commentId)
        {
            try
            {
                if (!await _commentService.DeleteAsync(commentId))
                    return StatusCode(500, "Internal Server Error");
                //log
                return Ok();
            } catch (NotFoundException ex)
            {
                //log
                return StatusCode(502, ex.Message);
            }
        }
    }
}
