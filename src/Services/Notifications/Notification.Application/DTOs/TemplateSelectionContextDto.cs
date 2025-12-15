using Notification.Domain.Enums;

namespace Notification.Application.DTOs;

public sealed record TemplateSelectionContextDto
(
    Guid UserId,
    decimal Amount,
    NotificationTypeEnum Type
);
