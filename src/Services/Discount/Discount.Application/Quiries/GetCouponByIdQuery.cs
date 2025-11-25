
using Discount.Application.DTOs;
using MediatR;

namespace Discount.Application.Quiries;

public record GetCouponByIdQuery( int Id) : IRequest<CouponDto>;
