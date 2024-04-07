using Microsoft.AspNetCore.Mvc;
using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Services.Interfaces;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpGet]   
        public async Task<IActionResult> GetAll() 
        {
            List<CommentR> comments = await _commentService.GetAllAsync();
            return Ok(comments);
        }
        [HttpPost]
        public async Task<IActionResult> Add(CommentW comment)
        {
            await _commentService.AddAsync(comment);
            return Ok();
        }
    }
}
