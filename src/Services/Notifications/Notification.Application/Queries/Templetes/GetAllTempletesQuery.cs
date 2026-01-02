
using MediatR;
using Notification.Domain.Entities;
using Notification.Domain.Enums;

namespace Notification.Application.Queries;

public record GetAllTempletesQuery(int? Type, int? channel,
    bool? IsActive ) : IRequest<IReadOnlyList<NotificationTemplate>>;
