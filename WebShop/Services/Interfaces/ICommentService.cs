using WebShop.Dtos.Read;
using WebShop.Dtos.Write;

namespace WebShop.Services.Interfaces
{
    public interface ICommentService
    {
        Task<CommentR> GetAsync(int id);
        Task<PaginationResponse<CommentR>> GetAllAsync(int page, int pageSize);
        Task<PaginationResponse<CommentR>> GetByUserAsync(string userId, int page, int pageSize);
        Task<PaginationResponse<CommentR>> GetByFeedbackAsync(int feedbackId, int page, int pageSize);
        Task<PaginationResponse<CommentR>> GetByParentCommentAsync(int parentCommentId, int page, int pageSize);
        Task<bool> AddAsync(CommentW comment);
        Task<bool> UpdateAsync(CommentW comment);
        Task<bool> DeleteAsync(int commentId);
    }
}
