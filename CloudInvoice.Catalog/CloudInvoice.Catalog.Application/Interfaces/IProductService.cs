using CloudInvoice.Catalog.Application.DTOs;

namespace CloudInvoice.Catalog.Application.Interfaces
{
    public interface IProductService
    {
        Task<PagedResultDto<ProductResponseDto>> GetProductsAsync(ProductQueryParameters parameters);
        Task<ProductResponseDto?> GetProductByIdAsync(Guid id);
        Task<ProductResponseDto> AddProductAsync(ProductCreateDto dto);
        Task<bool> UpdateProductAsync(Guid id, ProductUpdateDto dto);
        Task<bool> DeleteProductAsync(Guid id);
        Task<AvailabilityResponseDto> CheckAvailabilityAsync(Guid id);
        Task<bool> IsAvailableAsync(Guid id);
        Task<bool> DeactivateProductAsync(Guid id);
    }
}