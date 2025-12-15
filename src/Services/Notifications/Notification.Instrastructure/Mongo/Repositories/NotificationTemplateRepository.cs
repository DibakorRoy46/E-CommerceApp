
using MongoDB.Driver;
using Notification.Application.Interfaces.Repositories;
using Notification.Domain.Entities;
using Notification.Domain.Enums;
using Notification.Instrastructure.Mongo.Collections;

namespace Notification.Instrastructure.Mongo.Repositories;

public class NotificationTemplateRepository: INotificationTemplateRepository
{
    private readonly IMongoCollection<NotificationTemplateDocument> _collection;

    public NotificationTemplateRepository(NotificationMongoContext context)
    {
        _collection = context.Templates;
    }

    public async Task<IReadOnlyList<NotificationTemplate>> GetActiveTemplatesAsync(NotificationTypeEnum type,
        CancellationToken cancellationToken)
    {
        var documents = await _collection
            .Find(x => x.Type == type && x.IsActive)
            .ToListAsync(cancellationToken);

        return documents.Select(MapToDomain).ToList();
    }

    private static NotificationTemplate MapToDomain(NotificationTemplateDocument d)
    {
        return new NotificationTemplate(d.Type,d.Channel,d.TitleTemplate,d.ContentTemplate);
    }
}
