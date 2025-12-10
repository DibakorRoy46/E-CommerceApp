
using Notification.Domain.Enums;
using Notification.Domain.ValueObjects;

namespace Notification.Application.Interfaces;

public interface INotificationAppService
{
    Task<string> CreateNotificationAsync(string templateId, Recipient recipient, NotificationChannelEnum channel, IDictionary<string, object>? payload = null);
    Task RetryNotificationAsync(string notificationId);
}