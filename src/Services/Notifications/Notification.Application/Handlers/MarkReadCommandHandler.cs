
using MediatR;
using Notification.Application.Aggregates;
using Notification.Application.Commands;
using Notification.Domain.Exceptions;
using Notifications.Application.Interfaces;

namespace Notification.Application.Handlers;

public class MarkReadCommandHandler : IRequestHandler<MarkReadCommand, Unit>
{
    private readonly INotificationRepository _repo;

    public MarkReadCommandHandler(INotificationRepository repo) => _repo = repo;

    public async Task<Unit> Handle(MarkReadCommand request, CancellationToken ct)
    {
        var entity = await _repo.GetByIdAsync(request.NotificationId, ct)
        ?? throw new DomainException("Not found");

        var aggregate = new NotificationAggregate(entity);
        aggregate.MarkRead();

        entity.MarkRead();
        await _repo.UpdateStatusAsync(entity.Id, entity.Status, ct);
        return Unit.Value;
    }
}
