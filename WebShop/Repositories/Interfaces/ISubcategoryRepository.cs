using WebShop.Models;

namespace WebShop.Repositories.Interfaces
{
    public interface ISubcategoryRepository
    {
        Task<Subcategory> GetAsync(int id);
        Task<bool> SaveAsync();
    }
}
