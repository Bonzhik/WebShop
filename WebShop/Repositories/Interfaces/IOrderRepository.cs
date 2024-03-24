using WebShop.Models;

namespace WebShop.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetAsync(int id);
        Task<List<Order>> GetAllAsync();
        Task<List<Order>> GetByUserAsync(User user);
        Task<bool> AddAsync(Order order);
        Task<bool> UpdateAsync(Order order);
        Task<bool> DeleteAsync(Order order);
        Task<bool> SaveAsync();
    }
}
