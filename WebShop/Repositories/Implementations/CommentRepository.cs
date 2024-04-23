using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Models;
using WebShop.Repositories.Interfaces;

namespace WebShop.Repositories.Implementations
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationContext _db;
        public CommentRepository(ApplicationContext db)
        {
            _db = db;
        }
        public async Task<bool> AddAsync(Comment comment)
        {
            await _db.Comments.AddAsync(comment);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Comment comment)
        {
            _db.Comments.Remove(comment);
            return await SaveAsync();
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _db.Comments.ToListAsync();
        }

        public async Task<Comment> GetAsync(int id)
        {
            return await _db.Comments.FindAsync(id);
        }

        public async Task<List<Comment>> GetByFeedBackAsync(Feedback feedback)
        {
            return await _db.Comments.Where(c => c.Feedback.Equals(feedback)).ToListAsync();
        }

        public async Task<List<Comment>> GetByUserAsync(User user)
        {
            return await _db.Comments.Where(c => c.User.Equals(user)).ToListAsync();
        }
        public async Task<List<Comment>> GetByParentCommentAsync(Comment comment)
        {
            return await _db.Comments.Where(c => c.ParentComment.Equals(comment)).ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                //Логирование ошибки
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Comment comment)
        {
            _db.Comments.Update(comment);
            return await SaveAsync();
        }
    }
}
