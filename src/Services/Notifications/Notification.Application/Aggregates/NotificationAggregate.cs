
using Notification.Application.Events;
using Notification.Domain.Entities;

namespace Notification.Application.Aggregates;

public sealed class NotificationAggregate
{
    public Notifier Notification { get; private set; }
    private readonly List<object> _events = new();
    public IReadOnlyList<object> DomainEvents => _events.AsReadOnly();

    public NotificationAggregate(Notifier notification)
    {
        Notification = notification;
        _events.Add(new NotificationCreatedEvent(Guid.NewGuid().ToString(), notification.Id, notification.TemplateId, notification.Recipient.UserId, notification.Channel.ToString(),DateTime.UtcNow));
    }

    public void MarkSent(string provider)
    {
        Notification.MarkSent();
        _events.Add(new NotificationSentEvent(Guid.NewGuid().ToString(), Notification.Id, provider, DateTime.UtcNow));
    }

    public void MarkFailed(string provider, string reason)
    {
        Notification.MarkFailed();
        _events.Add(new NotificationFailedEvent(Guid.NewGuid().ToString(), Notification.Id, provider, reason, DateTime.UtcNow));
    }

    public void MarkDelivered() { Notification.MarkDelivered(); _events.Add(new NotificationDeliveredEvent(Guid.NewGuid().ToString(), Notification.Id, DateTime.UtcNow)); }
    public void MarkRead() { Notification.MarkRead(); _events.Add(new NotificationReadEvent(Guid.NewGuid().ToString(), Notification.Id, DateTime.UtcNow)); }
    public void Expire() { Notification.Expire(); _events.Add(new NotificationExpiredEvent(Guid.NewGuid().ToString(), Notification.Id, DateTime.UtcNow)); }

    public void ClearEvents() => _events.Clear();
}
