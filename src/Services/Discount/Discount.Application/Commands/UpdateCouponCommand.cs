

using Discount.Application.DTOs;
using MediatR;

namespace Discount.Application.Commands;

public record UpdateCouponCommand(int Id, string Name, string Code, string Description, decimal Amount, int IsActive,string ModifiedBy):IRequest<CouponDto>;
