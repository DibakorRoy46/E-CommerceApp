
using Discount.Application.DTOs;
using MediatR;

namespace Discount.Application.Quiries;

public record GetCouponByCodeQuery(string Code): IRequest<CouponDto>;
