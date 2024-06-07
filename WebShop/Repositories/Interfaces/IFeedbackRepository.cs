using WebShop.Models;

namespace WebShop.Repositories.Interfaces
{
    public interface IFeedbackRepository
    {
        Task<Feedback> GetAsync(int id);
        Task<IQueryable<Feedback>> GetAllAsync();
        Task<IQueryable<Feedback>> GetByUserAsync(User user);
        Task<IQueryable<Feedback>> GetByProductAsync(Product product);
        Task<bool> IsExists(Feedback feedback);
        Task<bool> AddAsync(Feedback feedback);
        Task<bool> UpdateAsync(Feedback feedback);
        Task<bool> DeleteAsync(Feedback feedback);
        Task<bool> SaveAsync();
    }
}
