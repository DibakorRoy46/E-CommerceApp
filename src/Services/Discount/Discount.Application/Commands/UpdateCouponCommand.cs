

using Discount.Application.DTOs;
using MediatR;

namespace Discount.Application.Commands;

public record UpdateCouponCommand(int Id, string Name, string Code, string Description, decimal Amount, int IsActive,
    DateTime StartDate,DateTime EndDate,string ModifiedBy):IRequest<CouponDto>;
