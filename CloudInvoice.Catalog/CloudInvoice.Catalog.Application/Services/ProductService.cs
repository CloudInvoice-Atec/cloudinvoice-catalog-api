using CloudInvoice.Catalog.Application.DTOs;
using CloudInvoice.Catalog.Domain.Entities;
using CloudInvoice.Catalog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudInvoice.Catalog.Application.Services
{
    public class ProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(ToDto);
        }

        public async Task<ProductResponseDto?> GetProductByIdAsync(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product is null ? null : ToDto(product);
        }

        public async Task<ProductResponseDto> AddProductAsync(ProductCreateDto dto)
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Code = dto.Code,
                Description = dto.Description,
                BasePrice = dto.BasePrice,
                TaxRate = dto.TaxRate,
                UnitOfMeasure = dto.UnitOfMeasure,
                IsActive = true
            };

            await _repository.AddAsync(product);
            return ToDto(product);
        }

        public async Task<bool> UpdateProductAsync(Guid id, ProductUpdateDto dto)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product is null) return false;

            product.Code = dto.Code;
            product.Description = dto.Description;
            product.BasePrice = dto.BasePrice;
            product.TaxRate = dto.TaxRate;
            product.UnitOfMeasure = dto.UnitOfMeasure;

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
                TaxRate = product.TaxRate
            };
        }
        private static ProductResponseDto ToDto(Product p) =>
            new(p.Id, p.Code, p.Description, p.BasePrice, p.TaxRate, p.UnitOfMeasure, p.IsActive);
    }
}
