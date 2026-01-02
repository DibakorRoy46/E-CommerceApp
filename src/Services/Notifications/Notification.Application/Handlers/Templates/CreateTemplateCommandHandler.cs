
using MediatR;
using Notification.Application.Commands;
using Notification.Application.Interfaces.Repositories;
using Notification.Domain.Entities;

namespace Notification.Application.Handlers;

public class CreateTemplateCommandHandler : IRequestHandler<CreateTemplateCommand, Guid>
{
    private readonly INotificationTemplateRepository _repository;

    public CreateTemplateCommandHandler(INotificationTemplateRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateTemplateCommand request,CancellationToken cancellationToken)
    {
        var template = new NotificationTemplate(
            request.TemplateCode,
            request.Type,
            request.Channel,
            request.TitleTemplate,
            request.ContentTemplate,
            request.Priority);

        await _repository.AddAsync(template, cancellationToken);

        return template.Id;
    }
}
