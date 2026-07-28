using CloudInvoice.Catalog.Domain.Entities;
using CloudInvoice.Catalog.Domain.Interfaces;
using CloudInvoice.Catalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudInvoice.Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogDbContext _context;

        public ProductRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Product> Items, int TotalCount)> GetPagedAsync(
            int page,
            int pageSize,
            Guid? categoryId,
            string? search,
            bool? isActive,
            decimal? minPrice,
            decimal? maxPrice)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (categoryId.HasValue)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            if (isActive.HasValue)
                query = query.Where(p => p.IsActive == isActive.Value);

            if (minPrice.HasValue)
                query = query.Where(p => p.BasePrice >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.BasePrice <= maxPrice.Value);

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                query = query.Where(p => EF.Functions.Like(p.Code, $"%{term}%")
                                       || EF.Functions.Like(p.Description, $"%{term}%"));
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(p => p.Code)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }


        public async Task<Product?> GetByIdAsync(Guid id) =>
            await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null) return;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
