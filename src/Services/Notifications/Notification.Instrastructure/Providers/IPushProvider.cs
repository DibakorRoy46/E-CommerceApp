
namespace Notification.Instrastructure.Providers;

public interface IPushProvider
{
    string Name { get; }
    Task<ProviderResult> SendAsync(string token, string title, string body, CancellationToken ct = default);
}
