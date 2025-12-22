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
    public async Task ExecuteAsync()
    {
        var messages = await _dbContext.OutboxMessages
            .FromSqlRaw("""
                SELECT TOP (20) *
                FROM OutboxMessages WITH (UPDLOCK, READPAST)
                WHERE Type = @type
                  AND ProcessedOn IS NULL
                  AND PoisonedOn IS NULL
                ORDER BY OccurredOn
            """, new SqlParameter("@type", OrderConstraints.OrderCreatedNotification))
            .AsTracking()
            .ToListAsync();

        foreach (var message in messages)
        {
            try
            {
                var orderCreatedMessageEvent = JsonSerializer.Deserialize<OrderCreatedMessageEvent>(message.Content);

                if (orderCreatedMessageEvent == null)
                    throw new InvalidOperationException("Invalid message content");

                await _publishEndpoint.Publish(orderCreatedMessageEvent);

                message.ProcessedOn = DateTime.UtcNow;
                message.Error = null;
            }
            catch (Exception ex)
            {
                message.RetryCount++;
                message.Error = ex.Message;

                if (message.RetryCount >= MaxRetryCount)
                {
                    message.PoisonedOn = DateTime.UtcNow;

                    _logger.LogCritical("POISON message detected. MessageId={Id}, Type={Type}",message.Id, message.Type);
                    continue;
                }

                _logger.LogWarning(ex, "Retry {Retry}/{MaxRetry} for OutboxMessage {Id}",message.RetryCount, MaxRetryCount, message.Id);
                throw;
            }
        }

        await _dbContext.SaveChangesAsync();
    }
}
