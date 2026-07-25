using CloudInvoice.Catalog.Application.DTOs;
using CloudInvoice.Catalog.Application.Interfaces;
using CloudInvoice.Catalog.Domain.Entities;
using CloudInvoice.Catalog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<CategoryResponseDto> AddCategoryAsync(CategoryCreateDto dto)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = dto.Name
            };

            await _repository.AddAsync(category);
            return new CategoryResponseDto(category.Id, category.Name);
        }

        public async Task<bool> UpdateCategoryAsync(Guid id, CategoryUpdateDto dto)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category is null) return false;

            category.Name = dto.Name;
            await _repository.UpdateAsync(category);
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category is null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}