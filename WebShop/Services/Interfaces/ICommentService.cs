using WebShop.Dtos.Read;
using WebShop.Dtos.Write;

namespace WebShop.Services.Interfaces
{
    public interface ICommentService
    {
        Task<CommentR> GetAsync(int id);
        Task<List<CommentR>> GetAllAsync();
        Task<List<CommentR>> GetByUserAsync(string userId);
        Task<List<CommentR>> GetByFeedbackAsync(int feedbackId);
        Task<List<CommentR>> GetByParentCommentAsync(int parentCommentId);
        Task<bool> AddAsync(CommentW comment);
        Task<bool> UpdateAsync(CommentW comment);
        Task<bool> DeleteAsync(int commentId);
    }
}
