
using MediatR;
using Notification.Application.Commands;
using Notification.Application.DTOs;
using Notification.Application.Interfaces.Repositories;
using Notification.Application.Interfaces.Services;
using Notification.Domain.Entities;
using Notification.Domain.Enums;
using Notification.Domain.Exceptions;
using Notification.Domain.ValueObjects;

namespace Notification.Application.Handlers;

public class CreatePaymentNotificationCommandHandler : IRequestHandler<CreatePaymentNotificationCommand>
{
    private readonly INotificationTemplateRepository _templateRepository;
    private readonly INotificationRepository _notificationRepository;
    private readonly ITemplateSelectionService _templateSelectionService;
    private readonly ITemplateRenderer _templateRenderer;
    private readonly INotificationSenderFactory _senderFactory;

    public CreatePaymentNotificationCommandHandler(
        INotificationTemplateRepository templateRepository,
        INotificationRepository notificationRepository,
        ITemplateSelectionService templateSelectionService,
        ITemplateRenderer templateRenderer,
        INotificationSenderFactory senderFactory)
    {
        _templateRepository = templateRepository;
        _notificationRepository = notificationRepository;
        _templateSelectionService = templateSelectionService;
        _templateRenderer = templateRenderer;
        _senderFactory = senderFactory;
    }

    public async Task Handle(CreatePaymentNotificationCommand command,CancellationToken cancellationToken)
    {
        // Load all active templates
        var templates = await _templateRepository.GetActiveTemplatesAsync(NotificationTypeEnum.PaymentSuccessful,cancellationToken);

        if (!templates.Any())
            throw new DomainException("No Existing Templete Founds");

        var context = new TemplateSelectionContextDto( command.UserId, command.Amount,
                                              NotificationTypeEnum.PaymentSuccessful);

        var selectedTemplates = _templateSelectionService.SelectTemplates(templates, context);

        //Process each template
        foreach (var template in selectedTemplates)
        {
            await CreateAndSendNotificationAsync(template,command,cancellationToken);
        }
    }

    private async Task CreateAndSendNotificationAsync(NotificationTemplate template,CreatePaymentNotificationCommand command,
        CancellationToken cancellationToken)
    {
        // Prepare template data
        var templateData = new TemplateData(new Dictionary<string, string>
        {
            { "UserName", command.UserName },
            { "OrderId", command.OrderId.ToString() },
            { "Amount", command.Amount.ToString("C") },
            { "Date", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm") }
        });

        // Render
        var title = _templateRenderer.Render(template.TitleTemplate,templateData);

        var content = _templateRenderer.Render(template.ContentTemplate,templateData);

        // Create domain entity
        var notification = new Notifier(
            command.UserId,
            command.OrderId,
            NotificationTypeEnum.PaymentSuccessful,
            template.Channel,
            title,
            content
        );

        // Persist snapshot
        await _notificationRepository.AddAsync(notification, cancellationToken);

        // Send
        var sender = _senderFactory.Create(template.Channel);
        var success = await sender.SendAsync(notification, cancellationToken);

        // Update status
        if (success)
            notification.MarkAsSent();
        else
            notification.MarkAsFailed();

        await _notificationRepository.UpdateAsync(notification, cancellationToken);
    }
}
