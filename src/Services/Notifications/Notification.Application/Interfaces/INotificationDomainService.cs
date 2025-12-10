
using Notification.Application.Aggregates;
using Notification.Domain.Enums;
using Notification.Domain.ValueObjects;

namespace Notification.Application.Interfaces;

public interface INotificationDomainService
{
    Task<NotificationAggregate> CreateNotificationAsync(string templateId, Recipient recipient, NotificationChannelEnum channel, IDictionary<string, object>? payload = null);
    Task RetryNotificationAsync(NotificationAggregate aggregate);
}