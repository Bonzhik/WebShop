using Microsoft.EntityFrameworkCore;
using WebShop.Data;
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
            _db.Feedbacks.Remove(feedback);
            return await SaveAsync();
        }

        public async Task<List<Feedback>> GetAllAsync()
        {
            return await _db.Feedbacks.ToListAsync();
        }

        public async Task<Feedback> GetAsync(int id)
        {
            return await _db.Feedbacks.FindAsync(id);
        }

        public async Task<List<Feedback>> GetByProductAsync(Product product)
        {
            return await _db.Feedbacks.Where(f => f.Product.Equals(product)).ToListAsync();
        }

        public async Task<List<Feedback>> GetByUserAsync(User user)
        {
            return await _db.Feedbacks.Where(f => f.User.Equals(user)).ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _db.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> UpdateAsync(Feedback feedback)
        {
            _db.Feedbacks.Update(feedback);
            return await SaveAsync();
        }
    }
}
