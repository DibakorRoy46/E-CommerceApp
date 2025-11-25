

using AutoMapper;
using Basket.Application.Commands;
using Basket.Application.Interfaces;
using Basket.Application.Responses;
using Basket.Domain.Entities;
using MediatR;

namespace Basket.Application.Handlers;

public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
{
    private readonly IBasketRepository _repo;
    private readonly IMapper _mapper;

    public CreateShoppingCartCommandHandler(IBasketRepository repo,IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<List<ShoppingCartItem>>(request.Items);

        ShoppingCart shoppingCart = new ShoppingCart(request.UserName,entity);

        var result = await _repo.UpsertBasketAsync(shoppingCart);

        return _mapper.Map<ShoppingCartResponse>(result);

    }
}
