using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Models;
using WebShop.Repositories.Interfaces;

namespace WebShop.Repositories.Implementations
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationContext _db;
        public CartRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<bool> AddAsync(Cart cart)
        {
            await _db.Carts.AddAsync(cart);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Cart cart)
        {
            _db.Carts.Remove(cart);
            return await SaveAsync();
        }

        public async Task<List<Cart>> GetAllAsync()
        {
            return await _db.Carts.ToListAsync();
        }

        public async Task<Cart> GetAsync(int id)
        {
            return await _db.Carts.FindAsync(id);
        }

        public async Task<Cart> GetByUserAsync(User user)
        {
            return await _db.Carts.FirstOrDefaultAsync(c => c.User.Equals(user));
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

        public async Task<bool> UpdateAsync(Cart cart)
        {
            _db.Carts.Update(cart);
            return await SaveAsync();
        }
    }
}
