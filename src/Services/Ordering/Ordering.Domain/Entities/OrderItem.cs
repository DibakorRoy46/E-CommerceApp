

namespace Ordering.Domain.Entities;

public class OrderItem
{
    public int OrderItemid { get; protected set; }
    public int OrderId { get; protected set; }
    public Order Order { get; protected set; }

    public string ProductId { get; protected set; }
    public string ProductName { get; protected set; }
    public string ProductCode { get; protected set; }
    public decimal UnitPrice { get; protected set; }
    public int Quantity { get; protected set; }
    public decimal ItemWiseDicount { get; protected set; }

    private OrderItem() { }

    public OrderItem(string productId, string productName, string productCode,
                     decimal unitPrice, int quantity, decimal discount)
    {
        ProductId = productId;
        ProductName = productName;
        ProductCode = productCode;
        UnitPrice = unitPrice;
        Quantity = quantity;
        ItemWiseDicount = discount;
    }

    public void Update(int quantity, decimal discount)
    {
        Quantity = quantity;
        ItemWiseDicount = discount;
    }
}

