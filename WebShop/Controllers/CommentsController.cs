using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Services.Interfaces;
using WebShop.Services.PaginationService;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IPaginationService<CommentR> _paginationsService;
        public CommentsController
            (
            ICommentService commentService,
            IPaginationService<CommentR>  paginationService
            )
        {
            _paginationsService = paginationService;
            _commentService = commentService;
        }
        [HttpGet]   
        public async Task<IActionResult> GetAll([FromQuery] int page, int pageSize) 
        {
            List<CommentR> comments = await _commentService.GetAllAsync();
            PaginationResponse<CommentR> result = _paginationsService.Paginate(comments, page, pageSize);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add(CommentW comment)
        {
            await _commentService.AddAsync(comment);
            return Ok();
        }
    }
}
