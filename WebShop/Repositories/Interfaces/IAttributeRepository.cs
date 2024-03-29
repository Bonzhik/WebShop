using WebShop.Models;

namespace WebShop.Repositories.Interfaces
{
    public interface IAttributeRepository
    {
        Task<Models.Attribute> GetAsync(int id);
        Task<List<AttributeValue>> GetValuesByProductAsync(Product product);
        Task<List<Models.Attribute>> GetByCategoryAsync(Category category);
    }
}
