
using MediatR;

namespace Notification.Application.Commands;

public record MarkDeliveredCommand(string NotificationId) : IRequest<Unit>;
