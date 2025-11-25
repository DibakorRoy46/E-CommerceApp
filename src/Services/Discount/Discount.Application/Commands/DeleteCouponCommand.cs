
using MediatR;

namespace Discount.Application.Commands;

public record DeleteCouponCommand(int Id):IRequest<bool>;
