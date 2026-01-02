
using Notification.Application.Interfaces.Services;
using Notification.Domain.Enums;

namespace Notification.Instrastructure.Services.Senders;

public class NotificationSenderFactory: INotificationSenderFactory
{
    public INotificationSender Create(NotificationChannelEnum channel)
        => channel switch
        {
            NotificationChannelEnum.Email => new EmailNotificationSender(),
            NotificationChannelEnum.Sms => new SmsNotificationSender(),
            NotificationChannelEnum.Push => new PushNotificationSender(),
            _ => throw new NotSupportedException()
        };
}
