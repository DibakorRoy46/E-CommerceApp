

using MediatR;

namespace Notification.Application.Commands;

public record CreatePaymentNotificationCommand(
    string UserId,
    int OrderId,
    decimal Amount,
    string UserName
):IRequest;
