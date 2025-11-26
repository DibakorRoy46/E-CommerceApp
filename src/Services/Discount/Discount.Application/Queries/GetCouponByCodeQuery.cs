
using Discount.Application.DTOs;
using MediatR;

namespace Discount.Application.Queries;

public record GetCouponByCodeQuery(string Code): IRequest<CouponDto>;
