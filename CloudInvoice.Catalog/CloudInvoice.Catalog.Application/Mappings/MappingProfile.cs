using AutoMapper;
using CloudInvoice.Catalog.Application.DTOs;
using CloudInvoice.Catalog.Domain.Entities;

namespace CloudInvoice.Catalog.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductResponseDto>()
                .ForCtorParam("Id", opt => opt.MapFrom(p => p.Id))
                .ForCtorParam("Code", opt => opt.MapFrom(p => p.Code))
                .ForCtorParam("Description", opt => opt.MapFrom(p => p.Description))
                .ForCtorParam("BasePrice", opt => opt.MapFrom(p => p.BasePrice))
                .ForCtorParam("TaxRate", opt => opt.MapFrom(p => p.TaxRate))
                .ForCtorParam("UnitOfMeasure", opt => opt.MapFrom(p => p.UnitOfMeasure))
                .ForCtorParam("IsActive", opt => opt.MapFrom(p => p.IsActive))
                .ForCtorParam("CategoryId", opt => opt.MapFrom(p => p.CategoryId))
                .ForCtorParam("CategoryName", opt => opt.MapFrom(p => p.Category != null ? p.Category.Name : string.Empty));

            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();

            CreateMap<Product, AvailabilityResponseDto>()
                .ForMember(d => d.IsAvailable, opt => opt.MapFrom(p => p.IsActive))
                .ForMember(d => d.BasePrice, opt => opt.MapFrom(p => p.BasePrice))
                .ForMember(d => d.TaxRate, opt => opt.MapFrom(p => p.TaxRate))
                .ForMember(d => d.ProductDescription, opt => opt.MapFrom(p => p.Description));

            CreateMap<Category, CategoryResponseDto>()
                .ForCtorParam("Id", opt => opt.MapFrom(c => c.Id))
                .ForCtorParam("Name", opt => opt.MapFrom(c => c.Name));

            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
        }
    }
}
