using WebShop.Dtos.Read;
using WebShop.Dtos.Write;
using WebShop.Models;

namespace WebShop.Services.Interfaces
{
    public interface ILogService
    {
        Task<bool> CreateLog(LogW log);
        Task<List<LogR>> GetAll();
        Task<List<LogR>> GetByUserName(string userName);
        Task<List<LogR>> GetByDate(DateTime date);
    }
}
