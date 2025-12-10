
using MediatR;
using Notification.Application.DTOs;

namespace Notification.Application.Queries;

public record GetNotificationByIdQuery(string Id) : IRequest<NotificationDto?>;