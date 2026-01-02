
using MediatR;
using Notification.Domain.Enums;

namespace Notification.Application.Commands;

public record CreateTemplateCommand(
    string TemplateCode,
    NotificationTypeEnum Type,
    NotificationChannelEnum Channel,
    string TitleTemplate,
    string ContentTemplate,
    int Priority,
    bool IsActive
    ) : IRequest<Guid>;
