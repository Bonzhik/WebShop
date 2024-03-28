using WebShop.Models;

namespace WebShop.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category> GetAsync(int id);
        Task<bool> SaveAsync();
    }
}
