
using MediatR;
using Notification.Application.Responses;

namespace Notification.Application.Queries;

public record GetUserNotificationsQuery(string UserId)
    : IRequest<IReadOnlyList<NotificationResponse>>;
