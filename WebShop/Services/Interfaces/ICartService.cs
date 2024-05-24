using WebShop.Dtos.Read;
using WebShop.Dtos.Write;

namespace WebShop.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartR> ClearAsync(string userId);
        Task<CartR> GetAsync(string userId);
        Task<CartR> UpdateAsync(CartW cart);
    }
}
