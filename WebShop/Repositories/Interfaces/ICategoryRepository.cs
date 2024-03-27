using WebShop.Models;

namespace WebShop.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAll();
        Task<bool> SaveAsync();
    }
}
