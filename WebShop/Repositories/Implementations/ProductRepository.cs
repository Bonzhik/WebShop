using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Models;
using WebShop.Repositories.Interfaces;

namespace WebShop.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _db;
        public ProductRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _db.Products.ToListAsync();
        }
        public async Task<Product> GetAsync(int id)
        {
            return await _db.Products.FindAsync(id);
        }
        public async Task<Product> GetNoTrackAsync(int id)
        {
            return await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<List<Product>> GetBySubcategoryAsync(Subcategory subcategory)
        {
            return await _db.Products.Where(p => p.Subcategory.Equals(subcategory)).ToListAsync();
        }
        public async Task<List<Product>> GetByCategoryAsync(Category category)
        {
            return await _db.Products.Where(p => p.Subcategory.Category.Equals(category)).ToListAsync();
        }
        public async Task<bool> AddAsync(Product product)
        {
            await _db.Products.AddAsync(product);
            return await SaveAsync();
        }
        public async Task<bool> UpdateAsync(Product product)
        {
            _db.Products.Update(product);
            return await SaveAsync();
        }
        public async Task<bool> DeleteAsync(Product product)
        {
            _db.Products.Remove(product);
            return await SaveAsync();
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

        public bool CheckEnoughProduct(Product product, int Quantity)
        {
            return product.Quantity > Quantity;
        }

        public async Task<bool> IsExists(Product product)
        {
            return await _db.Products.
                AnyAsync(p => p.Title.ToLower() == product.Title.ToLower() && p.Id != product.Id);
        }

        public async Task<List<Product>> Search(string search)
        {
            return await _db.Products.Where(p => EF.Functions.Like(p.Title, $"%{search}%")).ToListAsync();
        }

        public async Task<List<Product>> GetLatest()
        {
            return await _db.Products.OrderByDescending(p => p.CreatedAt).Take(10).ToListAsync();
        }
    }
}
