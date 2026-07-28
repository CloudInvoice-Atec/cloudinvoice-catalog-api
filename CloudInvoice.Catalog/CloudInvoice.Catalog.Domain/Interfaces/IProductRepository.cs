using CloudInvoice.Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudInvoice.Catalog.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<(IEnumerable<Product> Items, int TotalCount)> GetPagedAsync(
            int page,
            int pageSize,
            Guid? categoryId,
            string? search,
            bool? isActive,
            decimal? minPrice,
            decimal? maxPrice);

        Task<Product?> GetByIdAsync(Guid id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
    }
}
