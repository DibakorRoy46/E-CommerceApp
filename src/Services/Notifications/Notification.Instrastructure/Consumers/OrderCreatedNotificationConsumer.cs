using EventBus.Messages.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Instrastructure.Consumers;

internal class OrderCreatedNotificationConsumer : IConsumer<OrderCreatedEvent>
{

    public Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        throw new NotImplementedException();
    }
}
