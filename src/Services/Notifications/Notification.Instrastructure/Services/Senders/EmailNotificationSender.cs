
using Notification.Application.Interfaces.Services;
using Notification.Domain.Entities;

namespace Notification.Instrastructure.Services.Senders;

public class EmailNotificationSender : INotificationSender
{
    public async Task<bool> SendAsync(Notifier notification,CancellationToken cancellationToken)
    {
        // TODO: integrate SMTP / SendGrid
        await Task.Delay(100, cancellationToken);
        return true;
    }
}

