
using MediatR;

namespace Ordering.Application.Commands;

public sealed record DeleteOrderCommand( int OrderId, string UserName) : IRequest<bool>;
