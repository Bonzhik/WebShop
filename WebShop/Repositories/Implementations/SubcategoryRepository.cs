using WebShop.Data;
using WebShop.Models;
using WebShop.Repositories.Interfaces;

namespace WebShop.Repositories.Implementations
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly ApplicationContext _db;

        public SubcategoryRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<Subcategory> GetAsync(int id)
        {
            return await _db.Subcategories.FindAsync(id);
        }
        public async Task<bool> SaveAsync()
        {
            try
            {
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                //Логирование ошибки
                return false;
            }
        }
    }
}
