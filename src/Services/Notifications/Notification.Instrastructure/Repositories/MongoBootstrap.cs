
using MongoDB.Driver;
using Notification.Domain.Entities;
using Notification.Instrastructure.Interfaces;

namespace Notification.Instrastructure.Repositories;

public class MongoBootstrap : IMongoBootstrap
{
    private readonly IMongoDatabase _db;
    public MongoBootstrap(IMongoDatabase db) => _db = db;

    public async Task EnsureIndexesAsync(CancellationToken ct = default)
    {
        var read = _db.GetCollection<Notifier>("notifications_read");
        var indexKeys = Builders<Notifier>.IndexKeys.Ascending("Recipient.UserId").Ascending(n => n.Status).Descending(n => n.CreatedAt);
        await read.Indexes.CreateOneAsync(new CreateIndexModel<Notifier>(indexKeys), cancellationToken: ct);

        var delivery = _db.GetCollection<DeliveryEvent>("delivery_events");
        await delivery.Indexes.CreateOneAsync(new CreateIndexModel<DeliveryEvent>(Builders<DeliveryEvent>.IndexKeys.Ascending(d => d.NotificationId)), cancellationToken: ct);

        var outbox = _db.GetCollection<OutboxEvent>("outbox");
        var ttlIndex = new CreateIndexModel<OutboxEvent>(Builders<OutboxEvent>.IndexKeys.Ascending(o => o.CreatedAt),
            new CreateIndexOptions { ExpireAfter = TimeSpan.FromDays(30) });
        await outbox.Indexes.CreateOneAsync(ttlIndex, cancellationToken: ct);
    }
}
