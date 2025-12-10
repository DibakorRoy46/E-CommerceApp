

using Notification.Domain.Enums;

namespace Notification.Application.Responses;

public class NotificationResponse
{
    public string Id { get; set; } = default!;
    public string TemplateId { get; set; } = default!;
    public NotificationChannelEnum Channel { get; set; }
    public NotificationStatusEnum Status { get; set; }
    public IDictionary<string, object>? Payload { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
