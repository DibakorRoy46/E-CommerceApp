using EventBus.Messages.Events;
using Hangfire;
using MassTransit;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Application.Constraints;
using Ordering.Insfrastrueture.Presistence;
using System.Text.Json;

namespace Ordering.Insfrastrueture.Dispatcher;

public class OutboxOrderCreatedNotificationDispatcher
{
    private const int MaxRetryCount = 10;

    private readonly AppDbContext _dbContext;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<OutboxOrderCreatedNotificationDispatcher> _logger;

    public OutboxOrderCreatedNotificationDispatcher(AppDbContext dbContext,IPublishEndpoint publishEndpoint,
        ILogger<OutboxOrderCreatedNotificationDispatcher> logger)
    {
        _dbContext = dbContext;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    [AutomaticRetry(Attempts = 5, OnAttemptsExceeded = AttemptsExceededAction.Fail)]
    public async Task ExecuteAsync(CancellationToken cancellationToken=default)
    {
        var messages = await _dbContext.OutboxMessages
            .FromSqlRaw("""
                SELECT TOP (1000) *
                FROM OutboxMessages WITH (UPDLOCK, READPAST)
                WHERE Type = @type
                  AND ProcessedOn IS NULL
                  AND PoisonedOn IS NULL
                ORDER BY OccurredOn
            """, new SqlParameter("@type", OrderConstraints.OrderCreatedNotification))
            .AsTracking()
            .ToListAsync();

        var batchEvent = new OrderCreatedMessageEventBatch
        {
            BatchId = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            BatchItems = new List<OrderCreatedMessageEvent>()
        };

        foreach (var message in messages)
        {
            try
            {
                var payload =
                    JsonSerializer.Deserialize<OrderCreatedMessageEvent>(message.Content);

                if (payload == null)
                    throw new InvalidOperationException("Invalid content");

                batchEvent.BatchItems.Add(payload);
            }
            catch (Exception ex)
            {
                message.RetryCount++;
                message.Error = ex.Message;

                if (message.RetryCount >= MaxRetryCount)
                {
                    message.PoisonedOn = DateTime.UtcNow;
                    _logger.LogCritical( "POISON (serialization) OutboxId={Id}", message.Id);
                }
            }
        }

        // ❗ Nothing valid to publish
        if (batchEvent.BatchItems.Count == 0)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
            return;
        }

        try
        {
            await _publishEndpoint.Publish(batchEvent);

            // ✅ Mark ALL included rows as processed
            foreach (var item in batchEvent.BatchItems)
            {
                var msg = messages.First(x => x.CorrelationId == item.CorrelationId);
                msg.ProcessedOn = DateTime.UtcNow;
                msg.Error = null;
            }
        }
        catch (Exception ex)
        {
            // 🚨 Broker / network failure → retry ALL rows
            foreach (var message in messages)
            {
                message.RetryCount++;
                message.Error = ex.Message;

                if (message.RetryCount >= MaxRetryCount)
                {
                    message.PoisonedOn = DateTime.UtcNow;
                    _logger.LogCritical("POISON (batch) OutboxId={Id} & CorrelationId= {1}", message.Id, message.CorrelationId);
                }
            }

            throw;
        }

        await _dbContext.SaveChangesAsync();
    }
}
