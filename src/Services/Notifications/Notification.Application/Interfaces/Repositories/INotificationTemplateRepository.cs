
using Notification.Domain.Entities;
using Notification.Domain.Enums;

namespace Notification.Application.Interfaces.Repositories;

public interface INotificationTemplateRepository
{
    Task<IReadOnlyList<NotificationTemplate>> GetActiveTemplatesAsync(NotificationTypeEnum type,
                                            CancellationToken cancellationToken);
}