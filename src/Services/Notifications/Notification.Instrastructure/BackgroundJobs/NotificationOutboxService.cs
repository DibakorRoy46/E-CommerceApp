
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Notification.Application.Interfaces.Repositories;
using Notification.Application.Interfaces.Services;

namespace Notification.Instrastructure.BackgroundJobs;

public class NotificationOutboxService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<NotificationOutboxService> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(10);

    public NotificationOutboxService( IServiceProvider serviceProvider, ILogger<NotificationOutboxService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Notification Outbox Service started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var notificationRepository = scope.ServiceProvider.GetRequiredService<INotificationRepository>();
                var senderFactory = scope.ServiceProvider.GetRequiredService<INotificationSenderFactory>();

                var pendingNotifications = await notificationRepository.GetPendingNotificationsAsync(stoppingToken);

                foreach (var notification in pendingNotifications)
                {
                    try
                    {
                        var sender = senderFactory.Create(notification.Channel);
                        var success = await sender.SendAsync(notification, stoppingToken);

                        if (success)
                        {
                            notification.MarkAsSent();
                        }
                        else
                        {
                            notification.MarkAsFailed(retryDelaySeconds: 60);
                        }

                        await notificationRepository.UpdateAsync(notification, stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error sending notification {NotificationId}", notification.Id);
                        notification.MarkAsFailed(retryDelaySeconds: 60);
                        await notificationRepository.UpdateAsync(notification, stoppingToken);
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

