
using Notification.Application.Interfaces.Services;
using Notification.Domain.Entities;

namespace Notification.Instrastructure.Services.Senders;

public class PushNotificationSender : INotificationSender
{
    public async Task<bool> SendAsync(Notifier notification,CancellationToken cancellationToken)
    {
        // TODO: integrate Firebase / APNS
        await Task.Delay(30, cancellationToken);
        return true;
    }
}
