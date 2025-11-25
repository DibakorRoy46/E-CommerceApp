
namespace Basket.Application.Responses
{
    public record ShoppingCartResponse(string userName,List<ShoppingCartItemResponse> items)
    {

        public ShoppingCartResponse(string userName) : this(userName,new List<ShoppingCartItemResponse> ())
        {
            
        }
        public decimal TotalPrice => items.Sum(x => x.Quantity * x.Price);
    }
}
