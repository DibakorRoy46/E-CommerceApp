
using MongoDB.Driver;
using Notification.Application.Interfaces;
using Notification.Domain.Entities;

namespace Notification.Instrastructure.Repositories;

public class MongoDeliveryEventRepository : IDeliveryEventRepository
{
    private readonly IMongoCollection<DeliveryEvent> _col;
    public MongoDeliveryEventRepository(IMongoDatabase db) => _col = db.GetCollection<DeliveryEvent>("delivery_events");

    public async Task<DeliveryEvent?> GetByNotificationIdAsync(string notificationId, CancellationToken ct = default)
        => await _col.Find(d => d.NotificationId == notificationId).FirstOrDefaultAsync(ct);

    public async Task InsertAsync(DeliveryEvent deliveryEvent, CancellationToken ct = default)
        => await _col.InsertOneAsync(deliveryEvent, cancellationToken: ct);

    public async Task AddAttemptAsync(string notificationId, DeliveryAttempt attempt, CancellationToken ct = default)
    {
        var update = Builders<DeliveryEvent>.Update.Push(d => d.Attempts, attempt);
        var result = await _col.UpdateOneAsync(d => d.NotificationId == notificationId, update, cancellationToken: ct);
        if (result.MatchedCount == 0)
        {
            var ev = new DeliveryEvent(Guid.NewGuid().ToString(), notificationId);
            ev.AddAttempt(attempt.Provider, attempt.Succeeded, attempt.Response);
            await _col.InsertOneAsync(ev, cancellationToken: ct);
        }
    }
}
