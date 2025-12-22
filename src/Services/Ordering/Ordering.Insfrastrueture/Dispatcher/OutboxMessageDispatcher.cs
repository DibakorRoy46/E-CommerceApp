using EventBus.Messages.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ordering.Application.Constraints;
using Ordering.Insfrastrueture.Presistence;
using System.Text.Json;

namespace Ordering.Insfrastrueture.Dispatcher;

public class OutboxMessageDispatcher : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OutboxMessageDispatcher> _logger;

    public OutboxMessageDispatcher(IServiceProvider serviceProvider, ILogger<OutboxMessageDispatcher> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var publishEndPoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

            var pendingMessages = await dbContext.OutboxMessages
                .Where(x=>x.Type == OrderConstraints.OrderCreated && x.ProcessedOn == null)
                .OrderBy(x=>x.OccurredOn)
                .Take(20)
                .ToListAsync(stoppingToken);

            foreach (var message in pendingMessages)
            {
                try
                {
                    var orderCreatedEvent = JsonSerializer.Deserialize<OrderCreatedEvent>(message.Content);
                    if (orderCreatedEvent != null)
                    {
                        await publishEndPoint.Publish(orderCreatedEvent, stoppingToken);
                        await publishEndPoint.Publish(new OrderCreatedMessageEvent
                        {
                            OrderId = orderCreatedEvent.OrderId,
                            UserId = orderCreatedEvent.UserId,
                            UserName = orderCreatedEvent.UserName,
                            GrossValue = orderCreatedEvent.GrossValue,
                            NetValue = orderCreatedEvent.NetValue,
                            DiscountValue = orderCreatedEvent.DiscountValue,
                            NumberOfItems = orderCreatedEvent.NumberOfItems,
                            CustomerName = orderCreatedEvent.FirstName+" "+ orderCreatedEvent.LastName,
                            EmailAddress = orderCreatedEvent.EmailAddress,
                            OrderItems = orderCreatedEvent.OrderItems
                        }, stoppingToken);
                        message.ProcessedOn = DateTime.UtcNow;
                        _logger.LogInformation("Published outbox message with Id {MessageId}", message.Id);
                    }
                }
                catch (Exception ex)
                {
                    message.Error = ex.Message;
                    message.ProcessedOn = DateTime.UtcNow;
                    _logger.LogError(ex, "Error processing outbox message with Id {MessageId}", message.Id);
                }

            }
            await dbContext.SaveChangesAsync(stoppingToken);
            await Task.Delay(TimeSpan.FromSeconds(5000), stoppingToken);
        }
    }
}
