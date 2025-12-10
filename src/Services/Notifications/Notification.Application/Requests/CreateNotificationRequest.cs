
using Notification.Domain.Enums;

namespace Notification.Application.Requests;

public class CreateNotificationRequest
{
    public string TemplateId { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PushToken { get; set; }
    public NotificationChannelEnum Channel { get; set; }
    public IDictionary<string, object>? Payload { get; set; }
}