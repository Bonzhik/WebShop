using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Exceptions;
using WebShop.Models;
using WebShop.Repositories.Interfaces;

namespace WebShop.Repositories.Implementations
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ApplicationContext _db;
        public FeedbackRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<bool> AddAsync(Feedback feedback)
        {
            await _db.Feedbacks.AddAsync(feedback);
            return await SaveAsync();

        }

        public async Task<bool> DeleteAsync(Feedback feedback)
        {
            feedback.IsDeleted = true;
            _db.Feedbacks.Update(feedback);
            return await SaveAsync();
        }

        public async Task<List<Feedback>> GetAllAsync()
        {
            return await _db.Feedbacks.Where(c => c.IsDeleted == false).ToListAsync();
        }

        public async Task<Feedback> GetAsync(int id)
        {
            return await _db.Feedbacks.FindAsync(id);
        }

        public async Task<List<Feedback>> GetByProductAsync(Product product)
        {
            return await _db.Feedbacks.Where(f => f.Product.Equals(product)).Where(c => c.IsDeleted == false).ToListAsync();
        }

        public async Task<List<Feedback>> GetByUserAsync(User user)
        {
            return await _db.Feedbacks.Where(f => f.User.Equals(user)).ToListAsync();
        }

        public async Task<bool> IsExists(Feedback feedback)
        {
            return await _db.Feedbacks.AnyAsync(f => f.User.Id == feedback.User.Id && f.Product.Id == feedback.Product.Id);
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

        public async Task<bool> UpdateAsync(Feedback feedback)
        {
            if (_db.Feedbacks.Any(f => f.Id == feedback.Id))
            {
                _db.Feedbacks.Update(feedback);
                return await SaveAsync();
            }
            else
                throw new NotFoundException($"Отзыв {feedback.Id} не найден");
        }
    }
}
