using WebShop.Data;
using WebShop.Models;
using WebShop.Repositories.Interfaces;

namespace WebShop.Repositories.Implementations
{
    public class StatusRepository : IStatusRepository
    {
        private readonly ApplicationContext _db;
        public StatusRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<Status> GetAsync(int id)
        {
            return await _db.Statuses.FindAsync(id);
        }
    }
}
