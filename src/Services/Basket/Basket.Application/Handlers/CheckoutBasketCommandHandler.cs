
using AutoMapper;
using Basket.Application.Commands;
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

    public CheckoutBasketCommandHandler(IBasketRepository repo, ILogger<CheckoutBasketCommandHandler> logger,
        IPublishEndpoint publishEndpoint)
    {
        _repo = repo;
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Unit> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
    {
        var dto= request.checkoutDto;
        var basketEntity= await _repo.GetBasketByUserNameAsync(request.checkoutDto.UserName);
        if (basketEntity == null)
        {
            _logger.LogError("Basket not found for user: {UserName}", request.checkoutDto.UserName);
            throw new InvalidOperationException($"Basket not found for user: {request.checkoutDto.UserName}");
        }

        var eventMessage = dto.ToBasketCheckoutEvent(basketEntity);

        //publish the eventMessage to a message broker
        _logger.LogInformation("Basket checkout event created for user: {UserName}", request.checkoutDto.UserName);
        await _publishEndpoint.Publish(eventMessage, cancellationToken);

        //remove the basket
        await _repo.DeleteBasketByUserNameAsync(request.checkoutDto.UserName);

        return Unit.Value;
    }
}
