
using MediatR;
using Notification.Application.DTOs;
using Notification.Application.Queries;
using Notifications.Application.Interfaces;

namespace Notification.Application.Handlers;

public class GetNotificationByIdQueryHandler : IRequestHandler<GetNotificationByIdQuery, NotificationDto?>
{
    private readonly INotificationRepository _repo;


    public GetNotificationByIdQueryHandler(INotificationRepository repo) => _repo = repo;


    public async Task<NotificationDto?> Handle(GetNotificationByIdQuery request, CancellationToken ct)
    {
        var entity = await _repo.GetByIdAsync(request.Id, ct);
        
        if (entity is null) return null;

        return new NotificationDto(entity.Id, entity.TemplateId, entity.Channel, entity.Status, entity.CreatedAt,
            entity.UpdatedAt, entity.Payload);
    }
}
