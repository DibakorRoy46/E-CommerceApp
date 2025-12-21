
namespace EventBus.Messages.Events;

public class OrderCreatedMessageEvent : BaseIntegretionEvent
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public int OrderId { get; set; }    
    public decimal GrossValue { get; set; }
    public decimal NetValue { get; set; }
    public decimal DiscountValue { get; set; }
    public int NumberOfItems { get; set; }
    public string CustomerName { get; set; }
    public string EmailAddress { get; set; }
    public string Address { get; set; }
    public List<BasketItemEvent> OrderItems { get; set; } = new();
}
