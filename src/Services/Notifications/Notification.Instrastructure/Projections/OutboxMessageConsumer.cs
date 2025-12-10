
using MassTransit;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Notification.Domain.Entities;
using Notification.Domain.Enums;
using Notification.Instrastructure.Dispatcher;
using System.Text.Json;

namespace Notification.Instrastructure.Projections;

public class OutboxMessageConsumer : IConsumer<OutboxMessage>
{
    private readonly IMongoDatabase _db;
    private readonly ILogger<OutboxMessageConsumer> _logger;
    public OutboxMessageConsumer(IMongoDatabase db, ILogger<OutboxMessageConsumer> logger) { _db = db; _logger = logger; }

    public async Task Consume(ConsumeContext<OutboxMessage> context)
    {
        var message = context.Message;
        _logger.LogInformation("Projection consumer received {EventType}", message.EventType);

        if (message.EventType == "NotificationSent")
        {
            var json = JsonSerializer.Deserialize<JsonElement>(message.Payload);
            var notificationId = json.GetProperty("NotificationId").GetString();
            if (!string.IsNullOrEmpty(notificationId))
            {
                var readCol = _db.GetCollection<Notifier>("notifications_read");
                var update = Builders<Notifier>.Update.Set(n => n.Status, NotificationStatusEnum.Sent).Set(n => n.UpdatedAt, DateTime.UtcNow);
                await readCol.UpdateOneAsync(n => n.Id == notificationId, update, new UpdateOptions { IsUpsert = true });
            }
        }

        // handle other event types similarly (NotificationFailed, Delivered, Read)
    }
}
