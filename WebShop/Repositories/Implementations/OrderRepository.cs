using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Models;
using WebShop.Repositories.Interfaces;

namespace WebShop.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationContext _db;
        public OrderRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<bool> AddAsync(Order order)
        {
            await _db.Orders.AddAsync(order);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Order order)
        {
            order.IsDeleted = true;
            _db.Orders.Update(order);
            return await SaveAsync();
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _db.Orders.Where(c => c.IsDeleted == false).ToListAsync();
        }

        public async Task<Order> GetAsync(int id)
        {
            return await _db.Orders.FindAsync(id);
        }

        public async Task<List<Order>> GetByUserAsync(User user)
        {
            return await _db.Orders.Where(o => o.User.Equals(user)).Where(c => c.IsDeleted == false).ToListAsync();
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

        public async Task<bool> UpdateAsync(Order order)
        {
            _db.Orders.Update(order);
            return await SaveAsync();
        }
    }
}
