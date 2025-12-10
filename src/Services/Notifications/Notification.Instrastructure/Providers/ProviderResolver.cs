
using Microsoft.Extensions.Logging;
using Notification.Domain.Enums;

namespace Notification.Instrastructure.Providers;

public class ProviderResolver : IProviderResolver
{
    private readonly IEnumerable<IEmailProvider> _emails;
    private readonly IEnumerable<ISmsProvider> _sms;
    private readonly IEnumerable<IPushProvider> _push;
    private readonly ILogger<ProviderResolver> _logger;

    public ProviderResolver(IEnumerable<IEmailProvider> emails, IEnumerable<ISmsProvider> sms, IEnumerable<IPushProvider> push, ILogger<ProviderResolver> logger)
    {
        _emails = emails; _sms = sms; _push = push; _logger = logger;
    }

    public IEnumerable<object> GetProvidersForChannel(NotificationChannelEnum channel) =>
        channel switch
        {
            NotificationChannelEnum.Email => _emails.Cast<object>(),
            NotificationChannelEnum.Sms => _sms.Cast<object>(),
            NotificationChannelEnum.Push => _push.Cast<object>(),
            _ => Enumerable.Empty<object>()
        };
}