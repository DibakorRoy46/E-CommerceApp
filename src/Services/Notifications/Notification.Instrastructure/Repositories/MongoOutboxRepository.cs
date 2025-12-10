
using MongoDB.Driver;
using Notification.Domain.Entities;
using Notification.Instrastructure.Interfaces;

namespace Notification.Instrastructure.Repositories;

public class MongoOutboxRepository : IOutboxRepository
{
    private readonly IMongoCollection<OutboxEvent> _col;
    public MongoOutboxRepository(IMongoDatabase db) => _col = db.GetCollection<OutboxEvent>("outbox");

    public async Task AddAsync(OutboxEvent e, CancellationToken ct = default) =>
        await _col.InsertOneAsync(e, cancellationToken: ct);

    public async Task<List<OutboxEvent>> GetPendingAsync(int limit, CancellationToken ct = default) =>
        await _col.Find(o => o.Published == false).SortBy(o => o.CreatedAt).Limit(limit).ToListAsync(ct);

    public async Task MarkPublishedAsync(string id, CancellationToken ct = default) =>
        await _col.UpdateOneAsync(o => o.Id == id, Builders<OutboxEvent>.Update.Set(o => o.Published, true), cancellationToken: ct);
}
