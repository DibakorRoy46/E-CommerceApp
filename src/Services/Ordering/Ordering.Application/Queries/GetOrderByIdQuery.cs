
using MediatR;
using Ordering.Application.DTOs;

namespace Ordering.Application.Queries;

public record GetOrderByIdQuery(int OrderId, string UserId) : IRequest<OrderDto>;
