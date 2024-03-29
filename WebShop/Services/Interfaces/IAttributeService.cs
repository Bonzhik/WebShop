using WebShop.Dtos.Read;

namespace WebShop.Services.Interfaces
{
    public interface IAttributeService
    {
        Task<List<AttributeR>> GetByCategoryAsync(int productId);
    }
}
