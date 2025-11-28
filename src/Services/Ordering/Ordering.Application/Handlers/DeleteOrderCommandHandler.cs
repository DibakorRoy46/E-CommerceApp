

using MediatR;
using Ordering.Application.Commands;
using Ordering.Application.Exceptions;
using Ordering.Application.Repositories;

namespace Ordering.Application.Handlers;

internal class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly IOrderRepository _repo;

    public DeleteOrderCommandHandler(IOrderRepository repo)
    {
        _repo=repo;
    }

    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var entity= await _repo.GetOrderByIdAsync(request.OrderId, cancellationToken);
        if (entity is null)
        {
            throw new OrderNotFoundException(request.OrderId, request.UserName, string.Empty);
        }
        await _repo.DeleteOrderAsync(request.OrderId);
        await _repo.SaveChangesAsync(cancellationToken);
        return true;
    }
}
