
namespace EventBus.Messages.Events;

public class PaymentFailedEvent : BaseIntegretionEvent
{
    public string UserName { get; set; }
    public int OrderId { get; set; }
    public string Reason { get; set; }  
    public DateTimeOffset TimeStamp { get; set; }
}
