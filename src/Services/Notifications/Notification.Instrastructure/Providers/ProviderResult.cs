
namespace Notification.Instrastructure.Providers;

public record ProviderResult(bool Success, string? ProviderName = null, string? Response = null);