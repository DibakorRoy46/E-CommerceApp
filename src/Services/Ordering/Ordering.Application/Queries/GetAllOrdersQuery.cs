
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Specs;

namespace Ordering.Application.Queries;

public record GetAllOrdersQuery(OrderSpec OrderSpec): IRequest<List<OrderDto>>;
