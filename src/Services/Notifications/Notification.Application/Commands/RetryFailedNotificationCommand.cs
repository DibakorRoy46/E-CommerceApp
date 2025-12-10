
using MediatR;

namespace Notification.Application.Commands;

public record RetryFailedNotificationCommand(string NotificationId) : IRequest;
