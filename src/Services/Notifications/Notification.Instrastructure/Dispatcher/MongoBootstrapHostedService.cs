
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Notification.Instrastructure.Interfaces;

namespace Notification.Instrastructure.Dispatcher;

public class MongoBootstrapHostedService : IHostedService
{
    private readonly IMongoBootstrap _bootstrap;
    private readonly ILogger<MongoBootstrapHostedService> _logger;
    public MongoBootstrapHostedService(IMongoBootstrap bootstrap, ILogger<MongoBootstrapHostedService> logger)
    {
        _bootstrap = bootstrap; _logger = logger;
    }
    public async Task StartAsync(CancellationToken ct)
    {
        _logger.LogInformation("Ensuring MongoDB indexes...");
        await _bootstrap.EnsureIndexesAsync(ct);
        _logger.LogInformation("MongoDB bootstrap complete.");
    }
    public Task StopAsync(CancellationToken ct) => Task.CompletedTask;
}
