
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MassTransit.Mediator;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;

namespace Ordering.Insfrastrueture.MessageConsumer;

public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ILogger<BasketCheckoutConsumer> _logger;

    public BasketCheckoutConsumer(IMediator mediator, IMapper mapper, ILogger<BasketCheckoutConsumer> logger)
    {
        _mediator = mediator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        using var scope = _logger.BeginScope("Consuming Basket Checkout Event for {correlationId}", context.Message.CorrelationId);
        var cmd = _mapper.Map<CreateOrderCommand>(context.Message);
        await _mediator.Send(cmd);
        _logger.LogInformation("Basket Checkout Event completed!!!");
    }
}
