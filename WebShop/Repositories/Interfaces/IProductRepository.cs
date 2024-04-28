using WebShop.Models;

namespace WebShop.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(int id);
        Task<Product> GetNoTrackAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetBySubcategoryAsync(Subcategory subcategory);
        Task<List<Product>> GetByCategoryAsync(Category category);
        Task<List<Product>> GetLatest();
        Task<List<Product>> Search(string search);
        Task<bool> AddAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(Product product);
        Task<bool> IsExists(Product product);
        bool CheckEnoughProduct(Product product, int Quantity);
        Task<bool> SaveAsync();
    }
}
