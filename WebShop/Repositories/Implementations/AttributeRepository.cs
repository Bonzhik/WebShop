﻿using Microsoft.EntityFrameworkCore;
using WebShop.Data;
using WebShop.Models;
using WebShop.Repositories.Interfaces;

namespace WebShop.Repositories.Implementations
{
    public class AttributeRepository : IAttributeRepository
    {
        private readonly ApplicationContext _db;
        public AttributeRepository(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<Models.Attribute> GetAsync(int id)
        {
            return await _db.Attributes.FindAsync(id);
        }

        public async Task<List<Models.Attribute>> GetByCategoryAsync(Category category)
        {
            return await _db.Categories
                .Where(c => c.Id == category.Id)
                .SelectMany(c => c.Attributes)
                .ToListAsync();
        }

        public async Task<List<AttributeValue>> GetValuesByProductAsync(Product product)
        {
            return await _db.AttributeValues.Where(a => a.Product.Equals(product)).ToListAsync();
        }
    }
}
