
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Notification.Application.Interfaces.Repositories;
using Notification.Application.Interfaces.Services;

namespace Notification.Instrastructure.BackgroundJobs;

public class NotificationOutboxService : BackgroundService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly INotificationSenderFactory _senderFactory;
    private readonly ILogger<NotificationOutboxService> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(10);

    public NotificationOutboxService(INotificationRepository notificationRepository,INotificationSenderFactory senderFactory,
        ILogger<NotificationOutboxService> logger)
    {
        _notificationRepository = notificationRepository;
        _senderFactory = senderFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Notification Outbox Service started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var pendingNotifications = await _notificationRepository.GetPendingNotificationsAsync(stoppingToken);

                foreach (var notification in pendingNotifications)
                {
                    try
                    {
                        var sender = _senderFactory.Create(notification.Channel);
                        var success = await sender.SendAsync(notification, stoppingToken);

                        if (success)
                        {
                            notification.MarkAsSent();
                        }
                        else
                        {
                            notification.MarkAsFailed(retryDelaySeconds: 60);
                        }

                        await _notificationRepository.UpdateAsync(notification, stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error sending notification {NotificationId}", notification.Id);
                        notification.MarkAsFailed(retryDelaySeconds: 60);
                        await _notificationRepository.UpdateAsync(notification, stoppingToken);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing notification outbox.");
            }

            await Task.Delay(_interval, stoppingToken);
        }
    }
}

