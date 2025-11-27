
using Basket.Application.Interfaces;
using Basket.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Infrastructure.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _distributeCache;

    public BasketRepository(IDistributedCache distributedCache)
    {
        _distributeCache = distributedCache;
    }
    public async Task DeleteBasketByUserNameAsync(string userName)
    {
        await _distributeCache.RemoveAsync(userName);
    }

    public async Task<ShoppingCart> GetBasketByUserNameAsync(string userName)
    {
        var entity=await _distributeCache.GetStringAsync(userName);

        if (entity == null)
            return new ShoppingCart(userName);

        return JsonConvert.DeserializeObject<ShoppingCart>(entity) ;
    }

    public async Task<ShoppingCart> UpsertBasketAsync(ShoppingCart shoppingCart)
    {  
        await _distributeCache.SetStringAsync(shoppingCart.UserName, JsonConvert.SerializeObject(shoppingCart));
        return await GetBasketByUserNameAsync(shoppingCart.UserName);
    }
}
