using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Models;
using WebShop.Repositories.Interfaces;

namespace WebShop.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext db;
        public ProductRepository(ApplicationContext db)
        {
            this.db = db;
        }
        
        public async Task<List<Product>> GetAllAsync()
        {
            return await db.Products.ToListAsync();
        }
        public async Task<Product> GetAsync(int id)
        {
            return await db.Products.FindAsync(id);
        }
        public async Task<List<Product>> GetBySubcategoryAsync(Subcategory subcategory)
        {
            return await db.Products.Where(p => p.Subcategory.Equals(subcategory)).ToListAsync();
        }
        public async Task<List<Product>> GetByCategoryAsync(Category category)
        {
            return await db.Products.Where(p => p.Subcategory.Category.Equals(category)).ToListAsync();
        }
        public async Task<bool> AddAsync(Product product)
        {
            await db.Products.AddAsync(product);
            return await SaveAsync();
        }
        public async Task<bool> UpdateAsync(Product product)
        {
            db.Products.Update(product);
            return await SaveAsync();
        }
        public async Task<bool> DeleteAsync(Product product)
        {
            db.Products.Remove(product);
            return await SaveAsync();
        }
        public async Task<bool> SaveAsync()
        {
            return await db.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
