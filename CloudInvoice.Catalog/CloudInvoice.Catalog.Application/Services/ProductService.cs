using CloudInvoice.Catalog.Application.DTOs;
using CloudInvoice.Catalog.Application.Interfaces;
using CloudInvoice.Catalog.Domain.Entities;
using CloudInvoice.Catalog.Domain.Interfaces;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }


        public async Task<ProductResponseDto?> GetProductByIdAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product is null ? null : _mapper.Map<ProductResponseDto>(product);
        }

        public async Task<ProductResponseDto> AddProductAsync(ProductCreateDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);

            var product = _mapper.Map<Product>(dto);
            product.Id = Guid.NewGuid();
            product.IsActive = true;

            await _repository.AddAsync(product);

            product.Category = category;
            return _mapper.Map<ProductResponseDto>(product);
        }

        public async Task<bool> UpdateProductAsync(Guid id, ProductUpdateDto dto)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product is null) return false;

            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);

            _mapper.Map(dto, product);
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

            return _mapper.Map<AvailabilityResponseDto>(product);
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
                Items = items.Select(p => _mapper.Map<ProductResponseDto>(p)),
                TotalCount = totalCount,
                Page = parameters.Page,
                PageSize = parameters.PageSize
            };
        }
        
        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsUnpagedAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(p => _mapper.Map<ProductResponseDto>(p));
        }

        public async Task<IEnumerable<ProductResponseDto>> GetActiveProductsUnpagedAsync()
        {
            var products = await _repository.GetAllAsync(isActive: true);
            return products.Select(p => _mapper.Map<ProductResponseDto>(p));
        }
        
        


        // Removido ToDto em favor do AutoMapper
    }
}