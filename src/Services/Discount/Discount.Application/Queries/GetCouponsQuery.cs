
using Discount.Application.DTOs;
using MediatR;

namespace Discount.Application.Queries;

public record GetCouponsQuery(int? IsActive) : IRequest<List<CouponDto>>;
