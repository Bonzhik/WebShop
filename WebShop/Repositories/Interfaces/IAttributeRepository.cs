using WebShop.Models;

namespace WebShop.Repositories.Interfaces
{
    public interface IAttributeRepository
    {
        Task<Models.Attribute> GetAsync(int id);
        Task<List<AttributeValue>> GetByProductAsync(Product product);  
    }
}
