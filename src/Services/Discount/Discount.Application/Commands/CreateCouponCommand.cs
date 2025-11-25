
using Discount.Application.DTOs;
using MediatR;

namespace Discount.Application.Commands;

public record CreateCouponCommand(string Name,string Code,string Description,decimal Amount, int IsActive,string CreatedBy):IRequest<CouponDto>;
