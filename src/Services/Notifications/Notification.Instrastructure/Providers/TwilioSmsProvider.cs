
using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using System.Net.Http.Json;

namespace Notification.Instrastructure.Providers;

public class TwilioSmsProvider : ISmsProvider
{
    public string Name => "Twilio";
    private readonly HttpClient _http;
    private readonly ILogger<TwilioSmsProvider> _logger;
    private readonly AsyncRetryPolicy _retry;
    private readonly AsyncCircuitBreakerPolicy _circuit;

    public TwilioSmsProvider(HttpClient http, ILogger<TwilioSmsProvider> logger)
    {
        _http = http; _logger = logger;
        _retry = Policy.Handle<Exception>().WaitAndRetryAsync(2, i => TimeSpan.FromSeconds(Math.Pow(2, i)));
        _circuit = Policy.Handle<Exception>().CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
    }

    public async Task<ProviderResult> SendAsync(string phone, string message, CancellationToken ct = default)
    {
        try
        {
            return await _circuit.ExecuteAsync(async () =>
                await _retry.ExecuteAsync(async () =>
                {
                    var resp = await _http.PostAsJsonAsync("/Messages.json", new { to = phone, body = message }, ct);
                    var txt = await resp.Content.ReadAsStringAsync(ct);
                    return new ProviderResult(resp.IsSuccessStatusCode, Name, txt);
                }));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Twilio error");
            return new ProviderResult(false, Name, ex.Message);
        }
    }
}
