using WebShop.Models;

namespace WebShop.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<Comment> GetAsync(int id);
        Task<IQueryable<Comment>> GetAllAsync();
        Task<IQueryable<Comment>> GetByUserAsync(User user);
        Task<IQueryable<Comment>> GetByFeedBackAsync(Feedback feedback);
        Task<IQueryable<Comment>> GetByParentCommentAsync(Comment comment);
        Task<bool> AddAsync(Comment comment);
        Task<bool> UpdateAsync(Comment comment);
        Task<bool> DeleteAsync(Comment comment);
        Task<bool> SaveAsync();
    }
}
