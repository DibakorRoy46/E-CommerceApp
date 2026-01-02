using Notification.Application.DTOs;
using Notification.Application.Interfaces.Services;
using Notification.Domain.Entities;

namespace Notification.Application.Services;

public class DefaultTemplateSelectionService: ITemplateSelectionService
{
    public IReadOnlyList<NotificationTemplate> SelectTemplates(
        IReadOnlyList<NotificationTemplate> templates,
        TemplateSelectionContextDto context)
    {
        return templates
            .Where(t => t.IsActive)
            .ToList();
    }
}
