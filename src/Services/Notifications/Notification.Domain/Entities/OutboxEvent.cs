

namespace Notification.Domain.Entities;

public class OutboxEvent
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public string AggregateId { get; init; } = string.Empty;
    public string EventType { get; init; } = string.Empty;
    public object Payload { get; init; } = new { };
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public bool Published { get; init; } = false;
}
