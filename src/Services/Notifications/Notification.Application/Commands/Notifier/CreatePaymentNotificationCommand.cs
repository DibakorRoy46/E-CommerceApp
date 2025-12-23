

using MediatR;
using Notification.Domain.Enums;

namespace Notification.Application.Commands;

public record CreatePaymentNotificationCommand(
    string UserId,
    int OrderId,
    decimal Amount,
    string UserName,
    NotificationTypeEnum NotificationType
):IRequest;
