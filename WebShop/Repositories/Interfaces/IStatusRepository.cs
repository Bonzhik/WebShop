using WebShop.Models;

namespace WebShop.Repositories.Interfaces
{
    public interface IStatusRepository
    {
        Task<Status> GetAsync(int id);
    }
}
