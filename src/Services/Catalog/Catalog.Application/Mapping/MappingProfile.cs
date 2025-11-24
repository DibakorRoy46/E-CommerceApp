
using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.DTOs;
using Catalog.Domain.Entities;

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

    }
}
