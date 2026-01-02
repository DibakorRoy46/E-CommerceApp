using EventBus.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Notification.Instrastructure.Consumers;

public class OrderCreatedNotificationConsumer : IConsumer<OrderCreatedMessageEventBatch>
{
    private readonly ILogger<OrderCreatedNotificationConsumer> _logger;

    public OrderCreatedNotificationConsumer(ILogger<OrderCreatedNotificationConsumer> logger)
    {
        _logger = logger;
    }
    public Task Consume(ConsumeContext<OrderCreatedMessageEventBatch> context)
    {
        throw new NotImplementedException();
    }
}
