
using MediatR;

namespace Ordering.Application.Commands;

public record DeleteOrderCommand( int OrderId, string UserName) : IRequest<bool>;
