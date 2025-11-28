
using MediatR;
using Ordering.Application.DTOs;

namespace Ordering.Application.Commands;

public record UpdateOrderCommnad( OrderDto Order) : IRequest<OrderDto>;
