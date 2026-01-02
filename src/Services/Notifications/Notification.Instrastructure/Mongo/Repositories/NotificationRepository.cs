
using MongoDB.Driver;
using Notification.Application.Interfaces.Repositories;
using Notification.Domain.Entities;
using Notification.Domain.Enums;
using Notification.Instrastructure.Mongo.Collections;

namespace Notification.Instrastructure.Mongo.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly IMongoCollection<NotificationDocument> _collection;

    public NotificationRepository(NotificationMongoContext context)
    {
        _collection = context.Notifications;
    }

    public async Task AddAsync(Notifier notification,CancellationToken cancellationToken)
    {
        var document = MapToDocument(notification);

        await _collection.InsertOneAsync(document,cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Notifier notification,CancellationToken cancellationToken)
    {
        var document = MapToDocument(notification);

        await _collection.ReplaceOneAsync(x => x.Id == notification.Id,document,cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyList<Notifier>> GetPendingNotificationsAsync(CancellationToken cancellationToken)
    {
        var documents = await _collection.Find(n =>
                (n.Status == NotificationStatusEnum.Pending ||
                 n.Status == NotificationStatusEnum.Failed) &&
                (n.NextRetryAt == null || n.NextRetryAt <= DateTime.UtcNow))
            .ToListAsync(cancellationToken);

        return documents.Select(MapToDomain).ToList();
    }

    public async Task<IReadOnlyList<Notifier>> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        var documents = await _collection.Find(n => n.UserId == userId )
            .ToListAsync(cancellationToken);

        return documents.Select(MapToDomain).ToList();
    }

    private static NotificationDocument MapToDocument(Notifier n)
        => new()
        {
            Id = n.Id,
            UserId = n.UserId,
            OrderId = n.OrderId,
            Type = n.Type,
            Channel = n.Channel,
            Title = n.Title,
            Content = n.Content,
            Status = n.Status,
            CreatedAt = n.CreatedAt
        };

    private static Notifier MapToDomain(NotificationDocument d)
       => new Notifier(d.UserId,d.OrderId,d.Type,d.Channel,d.Title,d.Content,d.Status,d.RetryCount,d.NextRetryAt);

   
}
