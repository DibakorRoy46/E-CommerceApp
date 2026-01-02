
using Notification.Domain.Enums;

namespace Notification.Application.Interfaces.Services;

public interface INotificationSenderFactory
{
    INotificationSender Create(NotificationChannelEnum channel);
}
