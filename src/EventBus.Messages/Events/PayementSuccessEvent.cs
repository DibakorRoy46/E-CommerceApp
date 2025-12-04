
namespace EventBus.Messages.Events;

public class PayementSuccessEvent : BaseIntegretionEvent
{
    public string UserName { get; set; }
    public int OrderId { get; set; }
    public decimal TotolPrice { get; set; }
    public DateTimeOffset TimeStamp { get; set; }
}
