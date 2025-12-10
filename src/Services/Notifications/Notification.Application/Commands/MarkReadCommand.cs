using MediatR;

namespace Notification.Application.Commands;

public record MarkReadCommand(string NotificationId) : IRequest<Unit>;
