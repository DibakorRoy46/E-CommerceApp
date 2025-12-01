
namespace EventBus.Messages.Events;

public class BaseIntegretionEvent
{
    public Guid CorrelationId { get;  set; }
    public DateTime CreationDate { get;  set; }

    public BaseIntegretionEvent()
    {
        CorrelationId = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }

    public BaseIntegretionEvent(Guid correlationId, DateTime creationDate)
    {
        CorrelationId = correlationId;
        CreationDate = creationDate;
    }
}
