using WebShop.Dtos.Read;
using WebShop.Dtos.Write;

namespace WebShop.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductR> GetAsync(int id);
        Task<PaginationResponse<ProductR>> GetAllAsync(int page, int pageSize);
        Task<PaginationResponse<ProductR>> GetBySubcategoryAsync(int[] subcategoryId, int page, int pageSize);
        Task<PaginationResponse<ProductR>> GetByCategoryAsync(int[] categoryId, int page, int pageSize);
        Task<PaginationResponse<ProductR>> Search(string search, int page, int pageSize);
        Task<List<ProductR>> GetLatest();
        Task<bool> AddAsync(ProductW product);
        Task<bool> UpdateAsync(ProductW product);
        Task<bool> DeleteAsync(int productId);
    }
}
