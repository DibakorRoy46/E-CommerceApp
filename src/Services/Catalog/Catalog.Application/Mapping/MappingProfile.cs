
using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.DTOs;
using Catalog.Application.Responses;
using Catalog.Domain.Entities;
using Catalog.Domain.Enums;

namespace Catalog.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateProductHierarchyCommand, ProductHierarchy>();
        CreateMap<ProductHierarchyDto, ProductHierarchy>();
        CreateMap<ProductHierarchy, ProductHierarchyDto>();
        CreateMap<CreateBrandCommand, Brand>();
        CreateMap<BrandDto, Brand>();
        CreateMap<Brand, BrandDto>();
        CreateMap<Product, ProductDto>();
        CreateMap<ProductDto, Product>();
        CreateMap<CreateProductCommand, Product>();
        CreateMap<Product, CreateProductCommand>();

        CreateMap<ProductHierarchy, ProductHierarchyResponse>()
                .ForCtorParam("LevelName", opt => opt.MapFrom(src => src.LevelId.ToString()))
                 .ForCtorParam("ParentName",opt => opt.MapFrom(src => src.Parent != null ? src.Parent.Name : null));

    }
}
