

using AutoMapper;
using Basket.Application.Interfaces;
using Basket.Application.Queries;
using Basket.Application.Responses;
using MediatR;

namespace Basket.Application.Handlers;

public class GetBasketByUserNameQueryHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
{
    private readonly IBasketRepository _repo;
    private readonly IMapper _mapper;

    public GetBasketByUserNameQueryHandler(IBasketRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
    {
        var result=await _repo.GetBasketByUserNameAsync(request.UserName);

        if(result == null)
        {
            return new ShoppingCartResponse(request.UserName)
            {
                items = new List<ShoppingCartItemResponse>()
            };
        }

        return _mapper.Map<ShoppingCartResponse>(result);
    }
}
