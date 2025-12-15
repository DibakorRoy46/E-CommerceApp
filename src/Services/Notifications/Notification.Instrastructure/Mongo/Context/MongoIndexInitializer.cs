
using MongoDB.Driver;
using Notification.Instrastructure.Mongo.Collections;

namespace Notification.Instrastructure.Mongo;

public static class MongoIndexInitializer
{
    public static async Task CreateIndexesAsync(
        NotificationMongoContext context)
    {
        await CreateNotificationIndexes(context);
        await CreateTemplateIndexes(context);
    }

    private static async Task CreateNotificationIndexes(
        NotificationMongoContext context)
    {
        var collection = context.Notifications;

        // Outbox index
        var outboxIndex = new CreateIndexModel<NotificationDocument>(
            Builders<NotificationDocument>.IndexKeys
                .Ascending(x => x.Status)
                .Ascending(x => x.NextRetryAt));

        // User history index
        var userIndex = new CreateIndexModel<NotificationDocument>(
            Builders<NotificationDocument>.IndexKeys
                .Ascending(x => x.UserId)
                .Descending(x => x.CreatedAt));

        // Order index
        var orderIndex = new CreateIndexModel<NotificationDocument>(
            Builders<NotificationDocument>.IndexKeys
                .Ascending(x => x.OrderId));

        await collection.Indexes.CreateManyAsync(
            new[] { outboxIndex, userIndex, orderIndex });
    }

    private static async Task CreateTemplateIndexes(
        NotificationMongoContext context)
    {
        var collection = context.Templates;

        // Active template lookup
        var activeTemplateIndex = new CreateIndexModel<NotificationTemplateDocument>(
            Builders<NotificationTemplateDocument>.IndexKeys
                .Ascending(x => x.Type)
                .Ascending(x => x.IsActive)
                .Ascending(x => x.Priority));

        // Unique template code
        var templateCodeIndex = new CreateIndexModel<NotificationTemplateDocument>(
            Builders<NotificationTemplateDocument>.IndexKeys
                .Ascending(x => x.TemplateCode),
            new CreateIndexOptions { Unique = true });

        await collection.Indexes.CreateManyAsync(
            new[] { activeTemplateIndex, templateCodeIndex });
    }
}
