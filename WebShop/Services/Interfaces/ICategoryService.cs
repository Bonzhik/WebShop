using WebShop.Dtos.Read;
using WebShop.Models;

namespace WebShop.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryR>> GetAllAsync();
    }
}
