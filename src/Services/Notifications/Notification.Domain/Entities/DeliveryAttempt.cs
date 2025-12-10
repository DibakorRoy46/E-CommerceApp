
namespace Notification.Domain.Entities;

public sealed class DeliveryAttempt
{
    public DateTime AttemptedAt { get; init; } = DateTime.UtcNow;
    public string Provider { get; init; } = string.Empty;
    public bool Succeeded { get; init; }
    public string? Response { get; init; }
}
