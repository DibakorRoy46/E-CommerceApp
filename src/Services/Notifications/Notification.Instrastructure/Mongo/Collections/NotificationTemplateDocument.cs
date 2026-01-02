
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Notification.Domain.Enums;

namespace Notification.Instrastructure.Mongo.Collections;

public class NotificationTemplateDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } 

    public string TemplateCode { get; set; } = default!;
    public NotificationTypeEnum Type { get; set; }
    public NotificationChannelEnum Channel { get; set; }

    public string TitleTemplate { get; set; } = default!;
    public string ContentTemplate { get; set; } = default!;

    public int Priority { get; set; }
    public bool IsActive { get; set; }
}
