using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Exceptions;
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

        public async Task<IQueryable<Product>> GetAllAsync()
        {
            return _db.Products.Where(c => c.IsDeleted == false);
        }
        public async Task<Product> GetAsync(int id)
        {
            return await _db.Products.FindAsync(id);
        }
        public async Task<Product> GetNoTrackAsync(int id)
        {
            return await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IQueryable<Product>> GetBySubcategoryAsync(Subcategory subcategory)
        {
            return _db.Products.Where(p => p.Subcategory.Equals(subcategory)).Where(c => c.IsDeleted == false);
        }
        public async Task<IQueryable<Product>> GetByCategoryAsync(Category category)
        {
            return _db.Products.Where(p => p.Subcategory.Category.Equals(category)).Where(c => c.IsDeleted == false);
        }
        public async Task<bool> AddAsync(Product product)
        {
            await _db.Products.AddAsync(product);
            return await SaveAsync();
        }
        public async Task<bool> UpdateAsync(Product product)
        {
            if (_db.Products.Any(p => p.Id == product.Id))
            {
                _db.Products.Update(product);
                return await SaveAsync();
            }else 
                throw new NotFoundException($"Продукт {product.Id} не найден");
        }
        public async Task<bool> DeleteAsync(Product product)
        {
            product.IsDeleted = true;
            _db.Products.Update(product);
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

        public async Task<IQueryable<Product>> Search(string search)
        {
            return _db.Products.Where(p => EF.Functions.Like(p.Title, $"%{search}%")).Where(c => c.IsDeleted == false);
        }

        public async Task<List<Product>> GetLatest()
        {
            return await _db.Products.Where(c => c.IsDeleted == false).OrderByDescending(p => p.CreatedAt).Take(10).ToListAsync();
        }
    }
}
