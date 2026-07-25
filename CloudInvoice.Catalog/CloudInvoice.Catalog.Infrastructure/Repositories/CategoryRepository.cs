using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CloudInvoice.Catalog.Domain.Entities;
using CloudInvoice.Catalog.Domain.Interfaces;
using CloudInvoice.Catalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CloudInvoice.Catalog.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CatalogDbContext _context;

        public CategoryRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync() =>
            await _context.Categories.ToListAsync();

        public async Task<Category?> GetByIdAsync(Guid id) =>
            await _context.Categories.FindAsync(id);

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category is null) return;

            _context.Categories.Remove(category); // Se houver Products associados, a FK (Restrict) rejeita
            await _context.SaveChangesAsync();
        }
    }
}