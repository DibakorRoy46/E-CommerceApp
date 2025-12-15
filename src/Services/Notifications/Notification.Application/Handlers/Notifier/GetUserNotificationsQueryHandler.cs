
using MediatR;
using Notification.Application.Interfaces.Repositories;
using Notification.Application.Queries;
using Notification.Application.Responses;

namespace Notification.Application.Handlers;

public class GetUserNotificationsQueryHandler
    : IRequestHandler<GetUserNotificationsQuery, IReadOnlyList<NotificationResponse>>
{
    private readonly INotificationRepository _repository;

    public GetUserNotificationsQueryHandler(INotificationRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<NotificationResponse>> Handle(GetUserNotificationsQuery request,CancellationToken cancellationToken)
    {
        var notifications = await _repository.GetByUserIdAsync(request.UserId, cancellationToken);

        return notifications.Select(n => new NotificationResponse
        {
            Id = n.Id,
            Title = n.Title,
            Content = n.Content,
            Status = n.Status.ToString(),
            CreatedAt = n.CreatedAt
        }).ToList();
    }
}
