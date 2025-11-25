
using AutoMapper;
using Discount.Application.Commands;
using Discount.Application.DTOs;
using Discount.Domain.Entities;

namespace Discount.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CouponDto, Coupon>();
        CreateMap<Coupon,CouponDto>();
        CreateMap<Coupon, CreateCouponCommand>();
        CreateMap<CreateCouponCommand, Coupon>();
        CreateMap<Coupon, UpdateCouponCommand>();
        CreateMap<UpdateCouponCommand, Coupon>();
    }
}
