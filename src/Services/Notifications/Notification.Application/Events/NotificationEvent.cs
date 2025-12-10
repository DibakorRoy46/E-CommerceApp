
namespace Notification.Application.Events;

public record NotificationCreatedEvent(string CorrelationId, string NotificationId, string TemplateId, string UserId, string Channel, DateTime CreatedAt);
public record NotificationSentEvent(string CorrelationId, string NotificationId, string Provider, DateTime SentAt);
public record NotificationFailedEvent(string CorrelationId, string NotificationId, string Provider, string Reason, DateTime FailedAt);
public record NotificationDeliveredEvent(string CorrelationId, string NotificationId, DateTime DeliveredAt);
public record NotificationReadEvent(string CorrelationId, string NotificationId, DateTime ReadAt);
public record NotificationExpiredEvent(string CorrelationId, string NotificationId, DateTime ExpiredAt);