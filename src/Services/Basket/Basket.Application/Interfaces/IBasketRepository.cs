
using Basket.Domain.Entities;

namespace Basket.Application.Interfaces;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasketByUserNameAsync(string userName);
    Task<ShoppingCart> UpsertBasketAsync(ShoppingCart shoppingCart);
    Task DeleteBasketByUserNameAsync(string userName);
}
