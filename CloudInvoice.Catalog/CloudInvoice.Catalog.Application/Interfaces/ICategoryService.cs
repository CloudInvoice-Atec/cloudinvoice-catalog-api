using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CloudInvoice.Catalog.Application.DTOs;

namespace CloudInvoice.Catalog.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync();
        Task<CategoryResponseDto> AddCategoryAsync(CategoryCreateDto dto);
        Task<bool> UpdateCategoryAsync(Guid id, CategoryUpdateDto dto);
        Task<bool> DeleteCategoryAsync(Guid id);
    }
}