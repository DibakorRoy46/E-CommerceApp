
using Notification.Domain.Entities;
using Notification.Domain.Enums;

namespace Notifications.Application.Interfaces;

public interface INotificationRepository
{
    Task<Notifier?> GetByIdAsync(string id, CancellationToken ct = default);
    Task<IEnumerable<Notifier>> GetByUserIdAsync(string userId, CancellationToken ct = default);
    Task InsertAsync(Notifier notification, CancellationToken ct = default);
    Task UpdateStatusAsync(string id, NotificationStatusEnum status, CancellationToken ct = default);
}
