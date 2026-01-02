
using Notification.Application.DTOs;
using Notification.Domain.Entities;

namespace Notification.Application.Interfaces.Services;

public interface ITemplateSelectionService
{
    IReadOnlyList<NotificationTemplate> SelectTemplates(IReadOnlyList<NotificationTemplate> templates,
                                TemplateSelectionContextDto context);
}