using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CloudInvoice.Catalog.Application.DTOs;
using CloudInvoice.Catalog.Application.Interfaces;
using CloudInvoice.Catalog.Domain.Interfaces;

namespace CloudInvoice.Catalog.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllCategoriesAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Select(c => new CategoryResponseDto(c.Id, c.Name));
        }
    }
}