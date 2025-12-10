
using System.Text.Json;

namespace Notification.Instrastructure.DTOs;

public record NotificationCreatedDto(
    string NotificationId, 
    string TemplateId,
    string UserId, 
    string Channel,
    JsonElement Payload, 
    RecipientDto Recipient
    );