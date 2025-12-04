
namespace Basket.Domain.Entities;

public class ShoppingCartItem
{
    public int ProductId { get; set; }
    public string ImageFile { get; set; }
    public string ProductName { get; set; }
    public string ProductCode { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal ItemWiseDiscount { get; set; }
}
