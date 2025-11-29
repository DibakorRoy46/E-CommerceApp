
using MediatR;
using Ordering.Application.DTOs;

namespace Ordering.Application.Commands;

public record UpdateOrderCommand( OrderDto Order) : IRequest<OrderDto>;
