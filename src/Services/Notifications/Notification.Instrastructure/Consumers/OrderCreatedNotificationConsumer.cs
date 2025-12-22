using EventBus.Messages.Events;
using MassTransit;

namespace Notification.Instrastructure.Consumers;

internal class OrderCreatedNotificationConsumer : IConsumer<OrderCreatedEvent>
{

    public Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        throw new NotImplementedException();
    }
}
