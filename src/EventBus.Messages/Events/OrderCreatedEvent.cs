
namespace EventBus.Messages.Events;

public class OrderCreatedEvent
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public decimal GrossValue { get; set; }
    public decimal NetValue { get; set; }
    public decimal DiscountValue { get; set; }
    public int NumberOfItems { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string AddressLine { get; set; }
    public string Country { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }

    public string CardName { get; set; }
    public string CardNumber { get; set; }
    public string Expiration { get; set; }
    public string Cvv { get; set; }
    public int PaymentMethod { get; set; }
    public string Remarks { get; set; }
    public int OrderStatus { get; set; }
    public List<BasketItemEvent> OrderItems { get; set; } = new();
}
