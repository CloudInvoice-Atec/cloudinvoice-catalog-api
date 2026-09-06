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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository repository, ICategoryRepository categoryRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
        }


        public async Task<ProductResponseDto?> GetProductByIdAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product is null ? null : ToDto(product);
        }

        public async Task<ProductResponseDto> AddProductAsync(ProductCreateDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Code = dto.Code,
                Description = dto.Description,
                BasePrice = dto.BasePrice,
                TaxRate = dto.TaxRate,
                UnitOfMeasure = dto.UnitOfMeasure,
                IsActive = true,
                CategoryId = dto.CategoryId
            };

            await _repository.AddAsync(product);

            product.Category = category;
            return ToDto(product);
        }

        public async Task<bool> UpdateProductAsync(Guid id, ProductUpdateDto dto)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product is null) return false;

            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);

            product.Code = dto.Code;
            product.Description = dto.Description;
            product.BasePrice = dto.BasePrice;
            product.TaxRate = dto.TaxRate;
            product.UnitOfMeasure = dto.UnitOfMeasure;
            product.CategoryId = dto.CategoryId;
            product.Category = category;

            await _repository.UpdateAsync(product);
            return true;
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product is null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<AvailabilityResponseDto> CheckAvailabilityAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product is null)
            {
                return new AvailabilityResponseDto { IsAvailable = false, BasePrice = 0, TaxRate = 0 };
            }

            return new AvailabilityResponseDto
            {
                IsAvailable = product.IsActive,
                BasePrice = product.BasePrice,
                TaxRate = product.TaxRate,
                ProductDescription = product.Description
            };
        }

        public async Task<bool> IsAvailableAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product is not null && product.IsActive;
        }

        public async Task<bool> ToggleStatusAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product is null) return false;

            // Inverte o estado atual
            product.IsActive = !product.IsActive;

            await _repository.UpdateAsync(product);
            return true;
        }
        public async Task<PagedResultDto<ProductResponseDto>> GetProductsAsync(ProductQueryParameters parameters)
        {
            var (items, totalCount) = await _repository.GetPagedAsync(
                parameters.Page,
                parameters.PageSize,
                parameters.CategoryId,
                parameters.Search,
                parameters.IsActive,
                parameters.MinPrice,
                parameters.MaxPrice);

            return new PagedResultDto<ProductResponseDto>
            {
                Items = items.Select(ToDto),
                TotalCount = totalCount,
                Page = parameters.Page,
                PageSize = parameters.PageSize
            };
        }
        
        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsUnpagedAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(ToDto);
        }

        public async Task<IEnumerable<ProductResponseDto>> GetActiveProductsUnpagedAsync()
        {
            var products = await _repository.GetAllAsync(isActive: true);
            return products.Select(ToDto);
        }
        
        

        private static ProductResponseDto ToDto(Product p) =>
            new(p.Id, p.Code, p.Description, p.BasePrice, p.TaxRate, p.UnitOfMeasure, p.IsActive,
                p.CategoryId, p.Category?.Name ?? string.Empty);
    }
}