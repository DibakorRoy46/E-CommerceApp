
using AutoMapper;
using Basket.Application.Commands;
using Basket.Application.DTOs;
using Basket.Application.Interfaces;
using Basket.Application.Mapping;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Basket.Application.Handlers;
public class CheckoutBasketCommandHandler : IRequestHandler<CheckoutBasketCommand, Unit>
{
    private readonly IBasketRepository _repo;
    private readonly ILogger<CheckoutBasketCommandHandler> _logger;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;

    public CheckoutBasketCommandHandler(IBasketRepository repo, ILogger<CheckoutBasketCommandHandler> logger,
        IPublishEndpoint publishEndpoint, IMapper mapper)
    {
        _repo = repo;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
    {
        var dto=  _mapper.Map<BasketCheckoutDto>(request);

        var basketEntity= await _repo.GetBasketByUserNameAsync(request.UserName);
        if (basketEntity == null)
        {
            _logger.LogError("Basket not found for user: {UserName}", request.UserName);
            throw new InvalidOperationException($"Basket not found for user: {request.UserName}");
        }

        var eventMessage = dto.ToBasketCheckoutEvent(basketEntity);

        //publish the eventMessage to a message broker
        _logger.LogInformation("Basket checkout event created for user: {UserName}", request.UserName);
        await _publishEndpoint.Publish(eventMessage, cancellationToken);

        //remove the basket
        await _repo.DeleteBasketByUserNameAsync(request.UserName);

        return Unit.Value;
    }
}
