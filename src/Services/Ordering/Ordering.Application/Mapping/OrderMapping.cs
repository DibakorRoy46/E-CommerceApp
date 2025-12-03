
using Ordering.Application.Constraints;
using Ordering.Domain.Entities;
using System.Text.Json;

namespace Ordering.Application.Mapping;

public static class OrderMapping
{
    public static OutboxMessage MapOutboMessage( Order order)
    {
        return new OutboxMessage
        {
            CorrelationId = Guid.NewGuid().ToString(),
            Type = OrderConstraints.OrderCreated,
            Content = JsonSerializer.Serialize(order),
            OccurredOn = DateTime.UtcNow
        };
    }
}
