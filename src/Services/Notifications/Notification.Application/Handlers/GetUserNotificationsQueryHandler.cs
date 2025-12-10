
using MediatR;
using Notification.Application.DTOs;
using Notification.Application.Queries;
using Notifications.Application.Interfaces;

namespace Notification.Application.Handlers;

public class GetUserNotificationsQueryHandler : IRequestHandler<GetUserNotificationsQuery, IEnumerable<NotificationDto>>
{
    private readonly INotificationRepository _repository;


    public GetUserNotificationsQueryHandler(INotificationRepository repository)
    {
        _repository = repository;
    }


    public async Task<IEnumerable<NotificationDto>> Handle(GetUserNotificationsQuery request, CancellationToken ct)
    {
        var notifications = await _repository.GetByUserIdAsync(request.UserId, ct);


        return notifications.Select(n => new NotificationDto(n.Id,n.TemplateId,n.Channel,n.Status,n.CreatedAt,n.UpdatedAt,n.Payload));
    }
}