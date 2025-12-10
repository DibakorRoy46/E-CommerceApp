
using Notification.Domain.Enums;

namespace Notification.Application.DTOs;

public record NotificationDto(string Id,string TemplateId,NotificationChannelEnum Channel,NotificationStatusEnum Status,
    DateTime CreatedAt,DateTime? UpdatedAt,IReadOnlyDictionary<string, object>? Payload = null );