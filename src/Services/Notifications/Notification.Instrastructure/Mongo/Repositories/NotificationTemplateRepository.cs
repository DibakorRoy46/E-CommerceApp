
using MongoDB.Driver;
using Notification.Application.Interfaces.Repositories;
using Notification.Domain.Entities;
using Notification.Domain.Enums;
using Notification.Instrastructure.Mongo.Collections;

namespace Notification.Instrastructure.Mongo.Repositories;

public class NotificationTemplateRepository : INotificationTemplateRepository
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
        return new NotificationTemplate(d.TemplateCode, d.Type,d.Channel,d.TitleTemplate,d.ContentTemplate,d.Priority);
    }

    public async Task AddAsync(NotificationTemplate template, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(MapToDocument(template), cancellationToken: cancellationToken);
    }

    private static NotificationTemplateDocument MapToDocument(NotificationTemplate template)
    {
        return new NotificationTemplateDocument
        {
            TemplateCode = template.TemplateCode,
            Type = template.Type,
            Channel = template.Channel,
            TitleTemplate = template.TitleTemplate,
            ContentTemplate = template.ContentTemplate,
            IsActive = template.IsActive,
            Priority = template.Priority
        };
    }

    public async Task<NotificationTemplate?> GetByTemplateCodeAsync(string templateCode, CancellationToken cancellationToken)
    {
        var document = await _collection
            .Find(x => x.TemplateCode == templateCode)
            .FirstOrDefaultAsync(cancellationToken);

        return document == null ? null : MapToDomain(document);
    }

    public async Task UpdateAsync(NotificationTemplate template, CancellationToken cancellationToken)
    {      
        var update = Builders<NotificationTemplateDocument>.Update
            .Set(x => x.TitleTemplate, template.TitleTemplate)
            .Set(x => x.ContentTemplate, template.ContentTemplate)
            .Set(x => x.IsActive, template.IsActive)
            .Set(x => x.Priority, template.Priority);

        await _collection.UpdateOneAsync(x => x.TemplateCode == template.TemplateCode,update,cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyList<NotificationTemplate>> GetAllTemplatesAsync(int? type, int? channel,
        bool? isActive, CancellationToken cancellationToken = default)
    {
        var filterBuilder = Builders<NotificationTemplateDocument>.Filter;
        var filter = filterBuilder.Empty;
        if (type.HasValue)
        {
            filter &= filterBuilder.Eq(x => x.Type, (NotificationTypeEnum)type.Value);
        }
        if (channel.HasValue)
        {
            filter &= filterBuilder.Eq(x => x.Channel, (NotificationChannelEnum)channel.Value);
        }
        if (isActive.HasValue)
        {
            filter &= filterBuilder.Eq(x => x.IsActive, isActive.Value);
        }
        var documents = await _collection
            .Find(filter)
            .ToListAsync(cancellationToken);

        return documents.Select(MapToDomain).ToList();
    }
}
