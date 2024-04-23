using WebShop.Models;

namespace WebShop.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetAsync(int id);
        Task<List<Cart>> GetAllAsync();
        Task<Cart> GetByUserAsync(User user);
        Task<bool> AddAsync(Cart cart);
        Task<bool> UpdateAsync(Cart cart);
        Task<bool> DeleteAsync(Cart cart);
        Task<bool> DeleteItemAsync(CartProduct cartProduct);
        Task<bool> SaveAsync();
    }
}
