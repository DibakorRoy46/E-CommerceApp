
using MediatR;
using Notification.Application.Responses;

namespace Notification.Application.Queries;

public record GetUserNotificationsQuery(Guid UserId)
    : IRequest<IReadOnlyList<NotificationResponse>>;
