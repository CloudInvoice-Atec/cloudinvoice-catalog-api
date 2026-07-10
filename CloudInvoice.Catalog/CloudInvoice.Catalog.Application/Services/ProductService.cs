using CloudInvoice.Catalog.Application.DTOs;
using CloudInvoice.Catalog.Application.DTOs.CloudInvoice.Catalog.Application.DTOs;
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
            return products.Select(p => new ProductResponseDto(p.Id, p.Code, p.Description, p.BasePrice, p.TaxRate));
        }

        public async Task AddProductAsync(ProductCreateDto dto)
        {
           
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Code = dto.Code,
                Description = dto.Description,
                BasePrice = dto.BasePrice,
                TaxRate = dto.TaxRate
            };

            await _repository.AddAsync(product);
        }
    }
}
