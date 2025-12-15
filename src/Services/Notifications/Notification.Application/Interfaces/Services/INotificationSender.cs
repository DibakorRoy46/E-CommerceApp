
using Notification.Domain.Entities;

namespace Notification.Application.Interfaces.Services;

public interface INotificationSender
{
    Task<bool> SendAsync(Notifier notification, CancellationToken cancellationToken);
}
