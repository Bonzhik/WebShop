using WebShop.Dtos.Read;
using WebShop.Dtos.Write;

namespace WebShop.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task<FeedbackR> GetAsync(int id);
        Task<List<FeedbackR>> GetAllAsync();
        Task<List<FeedbackR>> GetByUserAsync(string userId);
        Task<List<FeedbackR>> GetByProductAsync(int productId);
        Task<bool> AddAsync(FeedbackW feedback);
        Task<bool> UpdateAsync(FeedbackW feedback);
        Task<bool> DeleteAsync(int feedbackId);
    }
}
