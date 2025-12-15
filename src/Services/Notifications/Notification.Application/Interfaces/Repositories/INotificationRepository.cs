
using Notification.Domain.Entities;

namespace Notification.Application.Interfaces.Repositories;

public interface INotificationRepository
{
    Task AddAsync(Notifier notification, CancellationToken cancellationToken);
    Task UpdateAsync(Notifier notification, CancellationToken cancellationToken);
    Task<IReadOnlyList<Notifier>> GetPendingNotificationsAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<Notifier>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}