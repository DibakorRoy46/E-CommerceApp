
using MediatR;
using Ordering.Application.DTOs;

namespace Ordering.Application.Commands;

public record CreateOrderCommand(OrderDto Order) : IRequest<OrderDto>;