

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Notification.Domain.Enums;

namespace Notification.Instrastructure.Mongo.Collections;

public class NotificationDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } 

    public string UserId { get; set; }

    public int OrderId { get; set; }

    public NotificationTypeEnum Type { get; set; }
    public NotificationChannelEnum Channel { get; set; }

    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;

    public NotificationStatusEnum Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public int RetryCount { get; set; } = 0;
    public DateTime? NextRetryAt { get; set; }

}
