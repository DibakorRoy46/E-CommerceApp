

using AutoMapper;
using Basket.Application.Commands;
using Basket.Application.GrpcService;
using Basket.Application.Interfaces;
using Basket.Application.Responses;
using Basket.Domain.Entities;
using MediatR;

namespace Basket.Application.Handlers;

public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
{
    private readonly IBasketRepository _repo;
    private readonly IMapper _mapper;
    private readonly DiscountGrpcService _grpcService;

    public CreateShoppingCartCommandHandler(IBasketRepository repo,IMapper mapper, DiscountGrpcService service)
    {
        _repo = repo;
        _mapper = mapper;
        _grpcService = service;
    }
    public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<List<ShoppingCartItem>>(request.Items);

        var coupon = await _grpcService.GetCouponByCode(request.CouponCode);
        decimal discount = 0;
        if (coupon != null)
        {
            discount =Convert.ToDecimal( coupon.Amount);
        }

        ShoppingCart shoppingCart = new ShoppingCart(request.UserName,entity, discount);

        var result = await _repo.UpsertBasketAsync(shoppingCart);

        ShoppingCartResponse response=  _mapper.Map<ShoppingCartResponse>(result);
        return response;
    }
}
