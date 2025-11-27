
using Discount.Application.DTOs;
using MediatR;

namespace Discount.Application.Queries;

public record GetCouponByIdQuery( int Id) : IRequest<CouponDto>;
