using WebShop.Dtos.Read;
using WebShop.Dtos.Write;

namespace WebShop.Services.Interfaces
{
    public interface IOrderService
    {
        Task<bool> AddAsync(OrderW order);
        Task<bool> UpdateAsync(OrderW order);
        Task<bool> DeleteAsync(int orderId);
        Task<List<OrderR>> GetAllAsync();
        Task<List<OrderR>> GetByUserAsync(string userId);
        Task<OrderR> GetAsync(int id);
    }
}
