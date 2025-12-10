
using MediatR;
using Notification.Application.Aggregates;
using Notification.Application.Commands;
using Notification.Domain.Exceptions;
using Notifications.Application.Interfaces;

namespace Notification.Application.Handlers;

public class MarkDeliveredCommandHandler : IRequestHandler<MarkDeliveredCommand,Unit>
{
    private readonly INotificationRepository _repository;

    public MarkDeliveredCommandHandler(INotificationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(MarkDeliveredCommand request, CancellationToken ct)
    {
        var notification = await _repository.GetByIdAsync(request.NotificationId, ct);
        if (notification == null) throw new Exception("Notification not found");

        var aggregate = new NotificationAggregate(notification);
        aggregate.MarkDelivered();

        await _repository.UpdateStatusAsync(notification.Id, notification.Status, ct);
        return Unit.Value;
    }
}