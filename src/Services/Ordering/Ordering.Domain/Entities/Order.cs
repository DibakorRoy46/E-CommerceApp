
using Ordering.Domain.Enums;

namespace Ordering.Domain.Entities;

public class Order : BaseEntity
{
    public int OrderId { get; protected set; }
    public string UserId { get; protected set; }
    public string? UserName { get; protected set; }
    public decimal? GrossValue { get; protected set; }
    public decimal? DiscountValue { get; protected set; }
    public decimal? NetValue { get; protected set; }
    public int NumberOfItems { get; protected set; }
    public string? FirstName { get; protected set; }
    public string? LastName { get; protected set; }
    public string? EmailAddress { get; protected set; }
    public string? AddressLine { get; protected set; }
    public string? Country { get; protected set; }
    public string? State { get; protected set; }
    public string? ZipCode { get; protected set; }
    public string? CardName { get; protected set; }
    public string? CardNumber { get; protected set; }
    public string? Expiration { get; protected set; }
    public string? Cvv { get; protected set; }
    public PaymentMethodEnum PaymentMethod { get; protected set; }
    public OrderStatusEnum Status { get; protected set; } = OrderStatusEnum.Pending;
    public string Remarks { get; protected set; }
    public List<OrderItem> OrderItems { get; protected set; } = new List<OrderItem>();


    private Order() { }

    public Order(string userId, string userName, decimal grossValue, decimal discountValue, decimal netValue, int numberOfItems,
         string firstName, string lastName, string email, string addressLine, string country, string state, string zipCode, string cardName,
            string cardNumber, string expiration, string cvv, PaymentMethodEnum paymentMethod )
    {
        UserId = userId;
        UserName = userName;
        GrossValue = grossValue;
        DiscountValue = discountValue;
        NetValue = netValue;
        NumberOfItems = numberOfItems;
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = email;
        AddressLine = addressLine;
        Country = country;
        State = state;
        ZipCode = zipCode;
        CardName = cardName;
        CardNumber = cardNumber;
        Expiration = expiration;
        Cvv = cvv;
        PaymentMethod = paymentMethod;
        Status = OrderStatusEnum.Pending;
        Remarks=string.Empty;
        SetCreated(userId);
    }

    public void Update(int id, OrderStatusEnum orderStatus, string remarks,string modifiedBy )
    {
        Id = id;
        Status = orderStatus;
        Remarks=  $"Order status changed to {orderStatus}.{remarks}";
        SetModified(modifiedBy);
    }

    public void AddItem(string productId, string productName, string productCode,
                    decimal unitPrice, int quantity, decimal discount)
    {
        var item = new OrderItem( productId, productName, productCode,
                                 unitPrice, quantity, discount);

        OrderItems.Add(item);

        NumberOfItems = OrderItems.Count;
        GrossValue += unitPrice * quantity;
        DiscountValue += discount;
        NetValue = GrossValue - DiscountValue;
    }

    public void UpdateItem(string productId, int quantity, decimal discount)
    {
        var item = OrderItems.FirstOrDefault(x => x.ProductId == productId);

        if (item != null)
        {
            item.Update(quantity, discount);

            GrossValue = OrderItems.Sum(x => x.UnitPrice * x.Quantity);
            DiscountValue = OrderItems.Sum(x => x.ItemWiseDicount);
            NetValue = GrossValue - DiscountValue;
        }
    }

}
