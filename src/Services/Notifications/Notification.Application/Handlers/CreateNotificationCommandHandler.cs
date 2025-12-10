
using MediatR;
using Notification.Application.Commands;
using Notification.Application.Interfaces;
using Notification.Domain.Entities;
using Notification.Domain.ValueObjects;
using Notifications.Application.Interfaces;

namespace Notification.Application.Handlers;

public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, string>
{
    private readonly INotificationDomainService _domainService;

    public CreateNotificationCommandHandler(INotificationDomainService domainService)
    {
        _domainService = domainService;
    }

    public async Task<string> Handle(CreateNotificationCommand request, CancellationToken ct)
    {
        var recipient = new Recipient(request.UserId, request.Email, request.PhoneNumber, request.PushToken);
        var aggregate = await _domainService.CreateNotificationAsync(request.TemplateId, recipient, request.Channel, request.Payload);
        return aggregate.Notification.Id;
    }
}
