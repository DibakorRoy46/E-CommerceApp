
namespace Basket.Domain.Entities;

public class ShoppingCart
{
    public string UserName { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    public decimal Discount { get; set; }
    private ShoppingCart() { }
    public ShoppingCart(string userName)
    {
        UserName = userName;
        Discount = 0;
    }

    public ShoppingCart(string userName, List<ShoppingCartItem> items, decimal discount )
    {
        UserName=userName;
        Items = items;
        Discount = discount;
    }
}
