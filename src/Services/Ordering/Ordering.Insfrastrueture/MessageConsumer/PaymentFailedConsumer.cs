

using EventBus.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using Ordering.Application.Repositories;
using Ordering.Domain.Enums;

namespace Ordering.Insfrastrueture.MessageConsumer;

public class PaymentFailedConsumer : IConsumer<PaymentFailedEvent>
{
    private readonly IOrderRepository _repo;
    private readonly ILogger<PaymentFailedConsumer> _logger;
    public PaymentFailedConsumer(IOrderRepository repo, ILogger<PaymentFailedConsumer> logger)
    {
        _repo = repo;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Payment failed event received for OrderId: {OrderId}, Reason: {Reason}", message.OrderId, message.Reason);
        var orderToUpdate = await _repo.GetOrderByIdAsync(message.OrderId);
        if (orderToUpdate == null)
        {
            _logger.LogWarning("OrderId: {OrderId}  and CorrelationId: {CorrelationId} status updated to 'Payment Failed'",
                message.OrderId, message.CorrelationId);
            return;
        }

        orderToUpdate.Update(orderToUpdate.OrderId, OrderStatusEnum.Failed, message.Reason, message.UserName);
        await _repo.UpdateOrderAsync(orderToUpdate);
        await _repo.SaveChangesAsync();
        _logger.LogInformation("OrderId: {OrderId}  and CorrelationId: {CorrelationId} status updated to 'Payment Paid'",
            message.OrderId, message.CorrelationId);
    }
}
