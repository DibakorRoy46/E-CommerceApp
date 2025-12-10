
namespace Notification.Instrastructure.Providers;

public interface ISmsProvider
{
    string Name { get; }
    Task<ProviderResult> SendAsync(string phone, string message, CancellationToken ct = default);
}
