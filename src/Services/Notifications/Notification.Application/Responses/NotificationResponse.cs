
namespace Notification.Application.Responses;

public class NotificationResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; } = default!;
    public string Content { get; init; } = default!;
    public string Status { get; init; } = default!;
    public DateTime CreatedAt { get; init; }
}