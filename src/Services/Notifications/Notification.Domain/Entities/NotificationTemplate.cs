
using Notification.Domain.Enums;

namespace Notification.Domain.Entities;

public class NotificationTemplate
{
    public Guid Id { get; private set; }
    public string TemplateCode { get; private set; }

    public NotificationTypeEnum Type { get; private set; }
    public NotificationChannelEnum Channel { get; private set; }

    public string TitleTemplate { get; private set; }
    public string ContentTemplate { get; private set; }

    public bool IsActive { get; private set; }
    public int Priority { get; private set; }

    private NotificationTemplate() { }

    public NotificationTemplate(string templateCode, NotificationTypeEnum type,NotificationChannelEnum channel,string titleTemplate,
        string contentTemplate, int priority)
    {
        Id = Guid.NewGuid();
        TemplateCode = templateCode;
        Type = type;
        Channel = channel;
        TitleTemplate = titleTemplate;
        ContentTemplate = contentTemplate;
        IsActive = true;
        Priority = priority;
    }

    public void Update(Guid id, string templateCode, NotificationTypeEnum type, NotificationChannelEnum channel, string titleTemplate,
        string contentTemplate,bool isActive, int priority)
    {
        Id = id;
        TemplateCode = templateCode;
        Type = type;
        Channel = channel;
        TitleTemplate = titleTemplate;
        ContentTemplate = contentTemplate;
        IsActive = isActive;
        Priority = priority;
    }
}
