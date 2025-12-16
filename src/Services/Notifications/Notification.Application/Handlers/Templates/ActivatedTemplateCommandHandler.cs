
using MediatR;
using Notification.Application.Commands;
using Notification.Application.Interfaces.Repositories;
using Notification.Domain.Entities;

namespace Notification.Application.Handlers;

public class ActivatedTemplateCommandHandler : IRequestHandler<ActivatedTemplateCommand, bool>
{
    private readonly INotificationTemplateRepository _repository;

    public ActivatedTemplateCommandHandler(INotificationTemplateRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(ActivatedTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await _repository.GetByTemplateCodeAsync(request.TemplateCode, cancellationToken);
        if (template is null)
        {
            return false;
        }

        template.Update(template.Id,template.TemplateCode,template.Type,template.Channel,template.TitleTemplate,
            template.ContentTemplate,isActive: true,template.Priority);

        await _repository.UpdateAsync(template, cancellationToken);
        return true;
    }
}
