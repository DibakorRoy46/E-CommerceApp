
using Notification.Domain.Enums;

namespace Notification.Domain.Entities;

public class NotificationTemplate
{
    public Guid Id { get; private set; }

    public NotificationTypeEnum Type { get; private set; }
    public NotificationChannelEnum Channel { get; private set; }

    public string TitleTemplate { get; private set; }
    public string ContentTemplate { get; private set; }

    public bool IsActive { get; private set; }

    private NotificationTemplate() { }

    public NotificationTemplate(NotificationTypeEnum type,NotificationChannelEnum channel,string titleTemplate,string contentTemplate)
    {
        Id = Guid.NewGuid();
        Type = type;
        Channel = channel;
        TitleTemplate = titleTemplate;
        ContentTemplate = contentTemplate;
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }
}
