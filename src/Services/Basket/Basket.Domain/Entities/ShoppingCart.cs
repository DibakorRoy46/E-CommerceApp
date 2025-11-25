
namespace Basket.Domain.Entities;

public class ShoppingCart
{
    public string UserName { get; set; }
    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    private ShoppingCart() { }
    public ShoppingCart(string userName)
    {
        UserName = userName;
    }

    public ShoppingCart(string userName, List<ShoppingCartItem> items)
    {
        UserName=userName;
        Items = items;
    }
}
