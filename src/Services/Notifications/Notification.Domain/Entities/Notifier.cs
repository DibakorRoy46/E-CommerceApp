
using Notification.Domain.Enums;
using Notification.Domain.Exceptions;
using Notification.Domain.ValueObjects;

namespace Notification.Domain.Entities;

public sealed class Notifier
{
    public string Id { get; private set; }
    public string TemplateId { get; private set; }
    public Recipient Recipient { get; private set; }
    public NotificationChannelEnum Channel { get; private set; }
    public NotificationStatusEnum Status { get; private set; } = NotificationStatusEnum.Pending;
    private readonly Dictionary<string, object> _payload = new();
    public IReadOnlyDictionary<string, object> Payload => _payload;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }


    public Notifier(string id, string templateId, NotificationChannelEnum channel, Recipient recipient, IDictionary<string, object>? payload = null)
    {
        Id = string.IsNullOrWhiteSpace(id) ? throw new ArgumentException("Id required") : id;
        TemplateId = string.IsNullOrWhiteSpace(templateId) ? throw new ArgumentException("TemplateId required") : templateId;
        Channel = channel;
        Recipient = recipient ?? throw new ArgumentNullException(nameof(recipient));
        if (payload != null) { foreach (var kv in payload) _payload[kv.Key] = kv.Value!; }
        CreatedAt = DateTime.UtcNow;
    }


    public void MarkSent() { Status = NotificationStatusEnum.Sent; UpdatedAt = DateTime.UtcNow; }
    public void MarkFailed() { Status = NotificationStatusEnum.Failed; UpdatedAt = DateTime.UtcNow; }
    public void MarkDelivered() { Status = NotificationStatusEnum.Delivered; UpdatedAt = DateTime.UtcNow; }
    public void MarkRead() { Status = NotificationStatusEnum.Read; UpdatedAt = DateTime.UtcNow; }
    public void Expire() { Status = NotificationStatusEnum.Expired; UpdatedAt = DateTime.UtcNow; }
}