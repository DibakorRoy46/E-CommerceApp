
namespace Ordering.Domain.Entities;

public class OutboxMessage : BaseEntity
{
    public int Id { get; set; }
    public string Type { get; set; }
    public string Content { get; set; }
    public Guid CorrelationId { get; set; }
    public DateTime OccurredOn { get; set; }
    public DateTime? ProcessedOn { get; set; }
    public bool? IsProcessed => ProcessedOn.HasValue;
    public string? Error { get; set; }
}
