
namespace EventBus.Messages.Events;

public class OrderCreatedMessageEventBatch
{
    public Guid BatchId { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OrderCreatedMessageEvent> BatchItems { get; set; } = new();
}
