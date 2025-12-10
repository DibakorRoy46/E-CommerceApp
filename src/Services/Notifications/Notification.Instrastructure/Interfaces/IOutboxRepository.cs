
using Notification.Domain.Entities;

namespace Notification.Instrastructure.Interfaces;

public interface IOutboxRepository
{
    Task AddAsync(OutboxEvent e, CancellationToken ct = default);
    Task<List<OutboxEvent>> GetPendingAsync(int limit, CancellationToken ct = default);
    Task MarkPublishedAsync(string id, CancellationToken ct = default);
}
