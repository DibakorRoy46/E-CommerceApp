
using Microsoft.Extensions.Logging;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using System.Net.Http.Json;

namespace Notification.Instrastructure.Providers;

public class SendGridEmailProvider : IEmailProvider
{
    public string Name => "SendGrid";
    private readonly HttpClient _http;
    private readonly ILogger<SendGridEmailProvider> _logger;
    private readonly AsyncRetryPolicy _retry;
    private readonly AsyncCircuitBreakerPolicy _circuit;

    public SendGridEmailProvider(HttpClient http, ILogger<SendGridEmailProvider> logger)
    {
        _http = http; _logger = logger;
        _retry = Policy.Handle<Exception>().WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(Math.Pow(2, i)));
        _circuit = Policy.Handle<Exception>().CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
    }

    public async Task<ProviderResult> SendAsync(string to, string subject, string bodyHtml, CancellationToken ct = default)
    {
        try
        {
            return await _circuit.ExecuteAsync(async () =>
                await _retry.ExecuteAsync(async () =>
                {
                    var payload = new
                    {
                        personalizations = new[] { new { to = new[] { new { email = to } } } },
                        from = new { email = "no-reply@yourdomain.com" },
                        subject,
                        content = new[] { new { type = "text/html", value = bodyHtml } }
                    };
                    var resp = await _http.PostAsJsonAsync("/v3/mail/send", payload, ct);
                    var text = await resp.Content.ReadAsStringAsync(ct);
                    return new ProviderResult(resp.IsSuccessStatusCode, Name, text);
                }));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "SendGrid error");
            return new ProviderResult(false, Name, ex.Message);
        }
    }
}
