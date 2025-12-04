
namespace Ordering.Application.Exceptions;

public class OrderNotFoundException : Exception
{
    public int OrderId { get; }
    public string? UserId { get; }
    public string ErrorCode { get; } = "ORDER_NOT_FOUND";
    public DateTime OccurredAt { get; } = DateTime.UtcNow;
    public string? CorrelationId { get; }

    public OrderNotFoundException(int orderId, string? userId = null, string? correlationId = null)
        : base(BuildMessage(orderId, userId, correlationId))
    {
        OrderId = orderId;
        UserId = userId;
        CorrelationId = correlationId;
    }

    public OrderNotFoundException(int orderId, Exception innerException, string? userId = null, string? correlationId = null)
        : base(BuildMessage(orderId, userId, correlationId), innerException)
    {
        OrderId = orderId;
        UserId = userId;
        CorrelationId = correlationId;
    }

    private static string BuildMessage(int orderId, string? userId, string? correlationId)
    {
        var message = $"Order with ID '{orderId}' was not found.";

        if (!string.IsNullOrEmpty(userId))
            message += $" User: '{userId}'.";

        if (!string.IsNullOrEmpty(correlationId))
            message += $" CorrelationId: '{correlationId}'.";

        message += " Please verify the request.";

        return message;
    }
}