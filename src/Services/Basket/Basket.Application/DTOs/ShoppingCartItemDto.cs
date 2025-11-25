

namespace Basket.Application.DTOs;

public class ShoppingCartItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }  
    public string ImageFile { get; set; }    
}
