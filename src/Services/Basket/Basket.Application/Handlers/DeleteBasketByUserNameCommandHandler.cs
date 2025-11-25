
using Basket.Application.Commands;
using Basket.Application.Interfaces;
using MediatR;

namespace Basket.Application.Handlers;

public class DeleteBasketByUserNameCommandHandler : IRequestHandler<DeleteBasketByUserNameCommand, Unit>
{
    private readonly IBasketRepository _repo;

    public DeleteBasketByUserNameCommandHandler(IBasketRepository repo)
    {
        _repo=repo;
    }

    public async Task<Unit> Handle(DeleteBasketByUserNameCommand request, CancellationToken cancellationToken)
    {
        var result = await _repo.GetBasketByUserNameAsync(request.UserName);

        if (result == null)
            throw new KeyNotFoundException($"Basket is not found by Username {request.UserName}");

        await _repo.DeleteBasketByUserNameAsync(request.UserName);

        return Unit.Value;
    }
}
