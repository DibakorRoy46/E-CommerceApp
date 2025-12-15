
using Notification.Application.Interfaces.Services;
using Notification.Domain.Entities;

namespace Notification.Instrastructure.Services.Senders;

public class SmsNotificationSender : INotificationSender
{
    public async Task<bool> SendAsync(Notifier notification,CancellationToken cancellationToken)
    {
        // TODO: integrate SMS provider
        await Task.Delay(50, cancellationToken);
        return true;
    }
}

