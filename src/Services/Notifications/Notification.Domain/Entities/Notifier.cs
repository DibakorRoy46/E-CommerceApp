
using Notification.Domain.Enums;

namespace Notification.Domain.Entities;

public sealed class Notifier
{
    public string Id { get; private set; }
    public string UserId { get; private set; }
    public int OrderId { get; private set; }
    public NotificationTypeEnum Type { get; private set; }
    public NotificationChannelEnum Channel { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public NotificationStatusEnum Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public int RetryCount { get; private set; } = 0;
    public DateTime? NextRetryAt { get; private set; }

    private Notifier() { } // For ORM / Mongo

    public Notifier(string userId,int orderId,NotificationTypeEnum type,NotificationChannelEnum channel,
        string title,string content)
    {
        Id = Guid.NewGuid().ToString();
        UserId = userId;
        OrderId = orderId;
        Type = type;
        Channel = channel;
        Title = title;
        Content = content;
        Status = NotificationStatusEnum.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    public Notifier(string userId, int orderId, NotificationTypeEnum type, NotificationChannelEnum channel,
       string title, string content,NotificationStatusEnum status, int retryCount, DateTime? nextRetryAt)
    {
        Id = Guid.NewGuid().ToString();
        UserId = userId;
        OrderId = orderId;
        Type = type;
        Channel = channel;
        Title = title;
        Content = content;
        Status = status;
        UpdatedAt = DateTime.UtcNow;
        RetryCount = retryCount;
        NextRetryAt = nextRetryAt;
    }

    public void MarkAsSent()
    {
        Status = NotificationStatusEnum.Sent;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsFailed(int retryDelaySeconds = 60)
    {
        Status = NotificationStatusEnum.Failed;
        RetryCount++;
        NextRetryAt = DateTime.UtcNow.AddSeconds(retryDelaySeconds);
    }

    public void MarkAsExpired()
    {
        Status = NotificationStatusEnum.Expired;
        UpdatedAt = DateTime.UtcNow;
    }
}