
using MediatR;
using Notification.Application.Interfaces.Repositories;
using Notification.Application.Queries;
using Notification.Domain.Entities;

namespace Notification.Application.Commands;

public class GetAllTempletesQueryHandler : IRequestHandler<GetAllTempletesQuery, IReadOnlyList<NotificationTemplate>>
{
    private readonly INotificationTemplateRepository _notificationTemplateRepository;
    public GetAllTempletesQueryHandler(INotificationTemplateRepository notificationTemplateRepository)
    {
        _notificationTemplateRepository = notificationTemplateRepository;
    }

    public async Task<IReadOnlyList<NotificationTemplate>> Handle(GetAllTempletesQuery request, CancellationToken cancellationToken)
    {
        var templates = await _notificationTemplateRepository.GetAllTemplatesAsync( request.Type, request.channel,request.IsActive, cancellationToken: cancellationToken);

        return templates;
    }
}
