
using Ordering.Application.Constraints;
using Ordering.Domain.Entities;
using System.Text.Json;

namespace Ordering.Application.Mapping;

public static class OrderMapping
{
    public static OutboxMessage MapOutboMessage( Order order,Guid correlationId)
    {
        return new OutboxMessage
        {
            CorrelationId = correlationId,
            Type = OrderConstraints.OrderCreated,
            Content = JsonSerializer.Serialize(order),
            OccurredOn = DateTime.UtcNow
        };
    }

    public static OutboxMessage MapNotificationOutboxMessage(Order order, Guid correlationId)
    {
        return new OutboxMessage
        {
            CorrelationId = correlationId,
            Type = OrderConstraints.OrderCreatedNotification,
            Content = JsonSerializer.Serialize(order),
            OccurredOn = DateTime.UtcNow
        };
    }
}
