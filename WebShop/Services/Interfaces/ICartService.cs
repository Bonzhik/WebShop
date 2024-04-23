using WebShop.Dtos.Read;
using WebShop.Dtos.Write;

namespace WebShop.Services.Interfaces
{
    public interface ICartService
    {
        Task<bool> ClearAsync(string userId);
        Task<CartR> GetAsync(string userId);
        Task<bool> UpdateAsync(CartW cart);
    }
}
