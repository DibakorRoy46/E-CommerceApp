

using MediatR;

namespace Notification.Application.Commands;

public record CreatePaymentNotificationCommand(
    Guid UserId,
    Guid OrderId,
    decimal Amount,
    string UserName
):IRequest;
