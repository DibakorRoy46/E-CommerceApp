
using Discount.Application.DTOs;
using MediatR;

namespace Discount.Application.Commands;

public record CreateCouponCommand(string Name,string Code,string Description,decimal Amount, int IsActive,DateTime StartDate,
    DateTime EndDate,string CreatedBy):IRequest<CouponDto>;
