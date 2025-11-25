
using AutoMapper;
using Basket.Application.DTOs;
using Basket.Application.Responses;
using Basket.Domain.Entities;

namespace Basket.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ShoppingCart, ShoppingCartResponse>();
        CreateMap<ShoppingCartItem, ShoppingCartItemResponse>();
        CreateMap<ShoppingCartItemDto, ShoppingCartItem>();
        CreateMap<ShoppingCartResponse, ShoppingCart>();
    }
}
