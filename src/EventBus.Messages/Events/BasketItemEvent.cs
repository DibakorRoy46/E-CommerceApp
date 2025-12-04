

namespace EventBus.Messages.Events;

public class BasketItemEvent
{
    public int ProductId { get; set; }
    public string ImageFile { get; set; }
    public string ProductName { get; set; }
    public string ProductCode { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal ItemWiseDiscount { get; set; }
}
