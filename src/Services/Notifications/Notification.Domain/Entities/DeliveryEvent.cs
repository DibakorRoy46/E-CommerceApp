
namespace Notification.Domain.Entities;

public sealed class DeliveryEvent
{
    public string Id { get; private set; }
    public string NotificationId { get; private set; }
    public List<DeliveryAttempt> Attempts { get; private set; } = new();
    public DateTime? DeliveredAt { get; private set; }
    public DateTime? ReadAt { get; private set; }


    public DeliveryEvent(string id, string notificationId)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        NotificationId = notificationId ?? throw new ArgumentNullException(nameof(notificationId));
    }


    public void AddAttempt(string provider, bool succeeded, string? response = null)
    {
        Attempts.Add(new DeliveryAttempt { Provider = provider, Succeeded = succeeded, Response = response });
        if (succeeded) DeliveredAt = DateTime.UtcNow;
    }

    public void MarkRead() => ReadAt = DateTime.UtcNow;
}