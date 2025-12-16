
using MediatR;
using Notification.Application.Commands;
using Notification.Application.Interfaces.Repositories;

namespace Notification.Application.Handlers;

public class DeactivatedTemplateCommandHandler : IRequestHandler<DeActivatedTemplateCommand, bool>
{
    private readonly INotificationTemplateRepository _repository;

    public DeactivatedTemplateCommandHandler(INotificationTemplateRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeActivatedTemplateCommand request, CancellationToken cancellationToken)
    {
        var template = await _repository.GetByTemplateCodeAsync(request.TemplateCode, cancellationToken);
        if (template is null)
        {
            return false;
        }

        template.Update(template.Id, template.TemplateCode, template.Type, template.Channel, template.TitleTemplate,
            template.ContentTemplate, isActive: false, template.Priority);

        await _repository.UpdateAsync(template, cancellationToken);
        return true;
    }
}
