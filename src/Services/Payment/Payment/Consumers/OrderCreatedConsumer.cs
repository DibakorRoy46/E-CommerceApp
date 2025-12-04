
using EventBus.Messages.Events;
using MassTransit;

namespace Payment.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    public readonly ILogger<OrderCreatedConsumer> _logger;

    public OrderCreatedConsumer(IPublishEndpoint publishEndpoint, ILogger<OrderCreatedConsumer> logger )
    {
        _logger = logger;
        _publishEndpoint = publishEndpoint;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var message = context.Message;

        _logger.LogInformation("OrderCreatedEvent consumed: {OrderId} - {UserId} - {GrossValue}",message.OrderId, message.UserId, message.NetValue);

        await Task.Delay(1000);

        // Simulate payment processing delay
        if(message.NetValue > 0)
        {
            var succeededEvent = new PayementSuccessEvent
            {
                UserName = message.UserName,
                OrderId = message.OrderId,
                CorrelationId = message.CorrelationId,
                TotolPrice = message.NetValue,
                TimeStamp = DateTime.UtcNow,
            };

            await _publishEndpoint.Publish(succeededEvent);

            _logger.LogInformation("Payment succeeded for Order: {OrderId} - {UserId} - {GrossValue}",message.OrderId, message.UserId, message.GrossValue);
        }
        else
        {
            var failedEvent = new PaymentFailedEvent
            {
                UserName = message.UserName,
                OrderId = message.OrderId,
                CorrelationId = context.CorrelationId.Value,
                Reason = "Total Value is less than zero or zero",
                TimeStamp = DateTime.UtcNow,
            };

            await _publishEndpoint.Publish(failedEvent);

            _logger.LogWarning("Payment failed for Order:{OrderId} - {UserId} - {GrossValue}", message.OrderId, message.UserId, message.GrossValue);    
        }
    }
}
