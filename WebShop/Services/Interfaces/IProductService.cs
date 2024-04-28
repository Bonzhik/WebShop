using WebShop.Dtos.Read;
using WebShop.Dtos.Write;

namespace WebShop.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductR> GetAsync(int id);
        Task<List<ProductR>> GetAllAsync();
        Task<List<ProductR>> GetBySubcategoryAsync(int[] subcategoryId);
        Task<List<ProductR>> GetByCategoryAsync(int[] categoryId);
        Task<List<ProductR>> Search(string search);
        Task<List<ProductR>> GetLatest();
        Task<bool> AddAsync(ProductW product);
        Task<bool> UpdateAsync(ProductW product);
        Task<bool> DeleteAsync(int productId);
    }
}
