
using MongoDB.Driver;
using Notification.Domain.Entities;
using Notification.Domain.Enums;
using Notifications.Application.Interfaces;

namespace Notification.Instrastructure.Repositories;

public class MongoNotificationRepository : INotificationRepository
{
    private readonly IMongoCollection<Notifier> _col;
    public MongoNotificationRepository(IMongoDatabase db) => _col = db.GetCollection<Notifier>("notifications_write");

    public async Task<Notifier?> GetByIdAsync(string id, CancellationToken ct = default)
        => await _col.Find(n => n.Id == id).FirstOrDefaultAsync(ct);

    public async Task InsertAsync(Notifier notification, CancellationToken ct = default)
        => await _col.InsertOneAsync(notification, cancellationToken: ct);

    public async Task UpdateStatusAsync(string id, NotificationStatusEnum status, CancellationToken ct = default)
    {
        var update = Builders<Notifier>.Update.Set(n => n.Status, status).Set(n => n.UpdatedAt, DateTime.UtcNow);
        await _col.UpdateOneAsync(n => n.Id == id, update, cancellationToken: ct);
    }

    // Additional convenience for Application Query handler (read projection uses separate collection,
    // but for small demos you might implement GetByUserId here)
    public async Task<IEnumerable<Notifier>> GetByUserIdAsync(string userId, CancellationToken ct = default)
    {
        return await _col.Find(n => n.Recipient.UserId == userId).SortByDescending(n => n.CreatedAt).Limit(100).ToListAsync(ct);
    }

}