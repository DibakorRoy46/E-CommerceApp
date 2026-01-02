using Notification.Domain.Enums;

namespace Notification.Application.DTOs;

public sealed record TemplateSelectionContextDto
(
    string UserId,
    decimal Amount,
    NotificationTypeEnum Type
);
