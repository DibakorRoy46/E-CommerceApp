

namespace Ordering.Application.DTOs;

public record OrderItemDto
{
    public string ProductId { get; init; }
    public string ProductName { get; init; }
    public string ProductCode { get; init; }
    public decimal UnitPrice { get; init; }
    public int Quantity { get; init; }
    public decimal ItemWiseDiscount { get; init; }
}
