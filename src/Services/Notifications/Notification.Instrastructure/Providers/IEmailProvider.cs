

namespace Notification.Instrastructure.Providers;

public interface IEmailProvider
{
    string Name { get; }
    Task<ProviderResult> SendAsync(string to, string subject, string bodyHtml, CancellationToken ct = default);
}
