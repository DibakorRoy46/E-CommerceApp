
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Notification.Instrastructure.Interfaces;
using System.Text.Json;

namespace Notification.Instrastructure.Dispatcher;

public class OutboxDispatcherHostedService : BackgroundService
{
    private readonly IOutboxRepository _outbox;
    private readonly IMessagePublisher _publisher;
    private readonly ILogger<OutboxDispatcherHostedService> _logger;

    public OutboxDispatcherHostedService(IOutboxRepository outbox, IMessagePublisher publisher, ILogger<OutboxDispatcherHostedService> logger)
    {
        _outbox = outbox; _publisher = publisher; _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Outbox dispatcher started");
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var pending = await _outbox.GetPendingAsync(50, stoppingToken);
                foreach (var e in pending)
                {
                    try
                    {
                        var wrapper = new OutboxMessage(e.EventType, JsonSerializer.Serialize(e.Payload));
                        await _publisher.PublishAsync(wrapper, stoppingToken);
                        await _outbox.MarkPublishedAsync(e.Id, stoppingToken);
                        _logger.LogInformation("Published outbox event {Id} ({Type})", e.Id, e.EventType);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed publishing outbox event {Id}", e.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Outbox dispatch loop error");
            }
            await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
        }
    }
}

public record OutboxMessage(string EventType, string Payload);
