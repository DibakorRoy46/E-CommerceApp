
using MediatR;
using Ordering.Application.DTOs;

namespace Ordering.Application.Queries;

public record GetOrderByUserNameQuery(string UserName): IRequest<List<OrderDto>>;