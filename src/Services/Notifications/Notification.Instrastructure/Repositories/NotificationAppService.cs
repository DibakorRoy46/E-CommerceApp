
using Notification.Application.Aggregates;
using Notification.Application.Interfaces;
using Notification.Domain.Enums;
using Notification.Domain.ValueObjects;
using Notifications.Application.Interfaces;

namespace Notification.Instrastructure.Repositories;

public class NotificationAppService : INotificationAppService
{
    private readonly INotificationDomainService _domainService;
    private readonly INotificationRepository _repository;

    public NotificationAppService(INotificationDomainService domainService, INotificationRepository repository)
    {
        _domainService = domainService;
        _repository = repository;
    }

    public async Task<string> CreateNotificationAsync(string templateId, Recipient recipient, NotificationChannelEnum channel, IDictionary<string, object>? payload = null)
    {
        var aggregate = await _domainService.CreateNotificationAsync(templateId, recipient, channel, payload);
        await _repository.InsertAsync(aggregate.Notification);
        return aggregate.Notification.Id;
    }

    public async Task RetryNotificationAsync(string notificationId)
    {
        var notification = await _repository.GetByIdAsync(notificationId);
        if (notification == null) throw new System.Exception("Notification not found");

        var aggregate = new NotificationAggregate(notification);
        await _domainService.RetryNotificationAsync(aggregate);
        await _repository.UpdateStatusAsync(notification.Id, notification.Status);
    }
}