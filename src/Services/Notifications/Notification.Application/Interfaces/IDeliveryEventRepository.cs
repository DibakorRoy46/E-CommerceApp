

using Notification.Domain.Entities;

namespace Notification.Application.Interfaces;

public interface IDeliveryEventRepository
{
    Task<DeliveryEvent?> GetByNotificationIdAsync(string notificationId, CancellationToken ct = default);
    Task InsertAsync(DeliveryEvent deliveryEvent, CancellationToken ct = default);
    Task AddAttemptAsync(string notificationId, DeliveryAttempt attempt, CancellationToken ct = default);
}
