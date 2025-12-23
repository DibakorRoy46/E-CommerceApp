
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using Ordering.Application.Repositories;
using Ordering.Domain.Enums;

namespace Ordering.Insfrastrueture.MessageConsumer;

public class PaymentSuccessConsumer : IConsumer<PayementSuccessEvent>
{
    private readonly IOrderRepository _repo;
    private readonly IUnitOfWork _unitofWork;
    private readonly ILogger<PaymentSuccessConsumer> _logger;

    public PaymentSuccessConsumer(IOrderRepository repo, ILogger<PaymentSuccessConsumer> logger,
        IUnitOfWork unitofWork)
    {
        _repo = repo;
        _logger = logger;
        _unitofWork = unitofWork;
    }
    public async Task Consume(ConsumeContext<PayementSuccessEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Payment S event received for OrderId: {OrderId}, Reason: {TotalPrice}", message.OrderId, message.TotolPrice);
        var orderToUpdate = await _repo.GetOrderByIdAsync(message.OrderId);
        if (orderToUpdate == null)
        {
            _logger.LogWarning("OrderId: {OrderId}  and CorrelationId: {CorrelationId} status updated to 'Payment Failed'",
                message.OrderId, message.CorrelationId);
            return;
        }

        orderToUpdate.Update(orderToUpdate.OrderId, OrderStatusEnum.Paid, "Successfully Paid", message.UserName);
        await _unitofWork.ExecuteInTransactionAsync(async (ct) =>
        {
            await _repo.UpdateOrderAsync(orderToUpdate);
            await _repo.SaveChangesAsync(ct);
        },CancellationToken.None);

        _logger.LogInformation("OrderId: {OrderId}  and CorrelationId: {CorrelationId} status updated to 'Payment Paid'",
            message.OrderId, message.CorrelationId);
    }
}
