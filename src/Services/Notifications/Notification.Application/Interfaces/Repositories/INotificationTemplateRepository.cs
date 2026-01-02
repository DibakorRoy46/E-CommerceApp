
using Notification.Domain.Entities;
using Notification.Domain.Enums;

namespace Notification.Application.Interfaces.Repositories;

public interface INotificationTemplateRepository
{
    Task<IReadOnlyList<NotificationTemplate>> GetActiveTemplatesAsync(NotificationTypeEnum type,CancellationToken cancellationToken = default);
    Task<IReadOnlyList<NotificationTemplate>> GetAllTemplatesAsync(int? type, int? channel,
                                                  bool? isActive, CancellationToken cancellationToken = default);
    Task<NotificationTemplate?> GetByTemplateCodeAsync(string templateCode, CancellationToken cancellationToken = default);
    Task AddAsync(NotificationTemplate template, CancellationToken cancellationToken = default);
    Task UpdateAsync(NotificationTemplate template, CancellationToken cancellationToken = default);
}