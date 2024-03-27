using WebShop.Models;

namespace WebShop.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> GetAsync(int id);
        Task<List<Comment>> GetAllAsync();
        Task<List<Comment>> GetByUserAsync(User user);
        Task<List<Comment>> GetByFeedBackAsync(Feedback feedback);
        Task<List<Comment>> GetByParrentCommentAsync(Comment comment);
        Task<bool> AddAsync(Comment comment);
        Task<bool> UpdateAsync(Comment comment);
        Task<bool> DeleteAsync(Comment comment);
        Task<bool> SaveAsync();
    }
}
