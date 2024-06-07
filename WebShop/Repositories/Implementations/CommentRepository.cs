using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Exceptions;
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
            comment.IsDeleted = true;
            _db.Comments.Update(comment);
            return await SaveAsync();
        }

        public async Task<IQueryable<Comment>> GetAllAsync()
        {
            return _db.Comments.Where(c => c.IsDeleted == false);
        }

        public async Task<Comment> GetAsync(int id)
        {
            return await _db.Comments.FindAsync(id);
        }

        public async Task<IQueryable<Comment>> GetByFeedBackAsync(Feedback feedback)
        {
            return _db.Comments.Where(c => c.Feedback.Equals(feedback)).Where(c => c.IsDeleted == false || c.ParentComment == null);
        }

        public async Task<IQueryable<Comment>> GetByUserAsync(User user)
        {
            return _db.Comments.Where(c => c.User.Equals(user));
        }
        public async Task<IQueryable<Comment>> GetByParentCommentAsync(Comment comment)
        {
            return _db.Comments.Where(c => c.ParentComment.Equals(comment)).Where(c => c.IsDeleted == false);
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
            if (_db.Comments.Any(c => c.Id == comment.Id))
            {
                _db.Comments.Update(comment);
                return await SaveAsync();
            }
            else
                throw new NotFoundException($"Комментарий {comment.Id} не найден");
        }
    }
}
