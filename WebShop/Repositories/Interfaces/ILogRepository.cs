using WebShop.Models;

namespace WebShop.Repositories.Interfaces
{
    public interface ILogRepository
    {
        Task<bool> CreateLog(Log log);
        Task<List<Log>> GetAll();
        Task<List<Log>> GetByUserName(string userName);
        Task<List<Log>> GetByDate(DateTime date);
    }
}
