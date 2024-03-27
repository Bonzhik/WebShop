using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Models;
using WebShop.Repositories.Interfaces;

namespace WebShop.Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationContext _db;

        public CategoryRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<List<Category>> GetAll()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _db.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
