
using Discount.Application.DTOs;
using MediatR;

namespace Discount.Application.Quiries;

public record GetCouponsQuery(int? IsActive) : IRequest<List<CouponDto>>;
