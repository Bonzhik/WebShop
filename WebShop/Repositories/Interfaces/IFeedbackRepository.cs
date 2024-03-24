using WebShop.Models;

namespace WebShop.Repositories.Interfaces
{
    public interface IFeedbackRepository
    {
        Task<Feedback> GetAsync(int id);
        Task<List<Feedback>> GetAllAsync();
        Task<List<Feedback>> GetByUserAsync(User user);
        Task<List<Feedback>> GetByProductAsync(Product product);
        Task<bool> AddAsync(Feedback feedback);
        Task<bool> UpdateAsync(Feedback feedback);
        Task<bool> DeleteAsync(Feedback feedback);
        Task<bool> SaveAsync();
    }
}
