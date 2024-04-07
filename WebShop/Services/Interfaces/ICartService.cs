using WebShop.Dtos.Write;
using WebShop.Models;

namespace WebShop.Services.Interfaces
{
    public interface ICartService
    {
        Task<bool> ClearAsync(string userId);
        Task<bool> GetAsync(string userId);
        Task<bool> UpdateAsync(CartW cart);
    }
}
