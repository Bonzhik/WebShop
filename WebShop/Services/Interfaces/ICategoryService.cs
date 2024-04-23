using WebShop.Dtos.Read;

namespace WebShop.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryR>> GetAllAsync();
    }
}
