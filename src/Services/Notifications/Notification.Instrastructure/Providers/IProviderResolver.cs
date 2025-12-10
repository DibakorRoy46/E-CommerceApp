
using Notification.Domain.Enums;

namespace Notification.Instrastructure.Providers;

public interface IProviderResolver
{
    IEnumerable<object> GetProvidersForChannel(NotificationChannelEnum channel);
}