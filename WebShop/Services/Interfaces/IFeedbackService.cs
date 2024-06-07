using WebShop.Dtos.Read;
using WebShop.Dtos.Write;

namespace WebShop.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task<FeedbackR> GetAsync(int id);
        Task<PaginationResponse<FeedbackR>> GetAllAsync(int page, int pageSize);
        Task<PaginationResponse<FeedbackR>> GetByUserAsync(string userId, int page, int pageSize);
        Task<PaginationResponse<FeedbackR>> GetByProductAsync(int productId, int page, int pageSize);
        Task<bool> AddAsync(FeedbackW feedback);
        Task<bool> UpdateAsync(FeedbackW feedback);
        Task<bool> DeleteAsync(int feedbackId);
    }
}
