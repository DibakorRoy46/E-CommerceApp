
using MediatR;
using Notification.Application.DTOs;

namespace Notification.Application.Queries;

public record GetUserNotificationsQuery(string UserId) : IRequest<IEnumerable<NotificationDto>>;