
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System.Net.Http.Json;

namespace Notification.Instrastructure.Providers;

public class FcmPushProvider : IPushProvider
{
    public string Name => "FCM";
    private readonly HttpClient _http;
    private readonly ILogger<FcmPushProvider> _logger;
    private readonly AsyncRetryPolicy _retry;

    public FcmPushProvider(HttpClient http, ILogger<FcmPushProvider> logger)
    {
        _http = http; _logger = logger;
        _retry = Policy.Handle<Exception>().RetryAsync(2);
    }

    public async Task<ProviderResult> SendAsync(string token, string title, string body, CancellationToken ct = default)
    {
        try
        {
            return await _retry.ExecuteAsync(async () =>
            {
                var payload = new { to = token, notification = new { title, body } };
                var resp = await _http.PostAsJsonAsync("/fcm/send", payload, ct);
                var txt = await resp.Content.ReadAsStringAsync(ct);
                return new ProviderResult(resp.IsSuccessStatusCode, Name, txt);
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "FCM error");
            return new ProviderResult(false, Name, ex.Message);
        }
    }
}
