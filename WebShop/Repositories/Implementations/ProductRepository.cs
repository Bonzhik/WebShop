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
            return await _db.SaveChangesAsync() > 0 ? true : false;
        }

        public bool CheckEnoughProduct(Product product, int Quantity)
        {
            return product.Quantity > Quantity;
        }
    }
}
