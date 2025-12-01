

using Basket.Application.DTOs;
using Basket.Application.Responses;
using Basket.Domain.Entities;
using EventBus.Messages.Events;

namespace Basket.Application.Mapping;

public static class BasketMapping
{
    public static ShoppingCart ToEntity(this ShoppingCartResponse response)
    {
        return new ShoppingCart(response.userName)
        {
            Items = response.items.Select(item => new ShoppingCartItem
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Price = item.Price,
                Quantity = item.Quantity
            }).ToList()
        };
    }

    public static BasketCheckoutEvent ToBasketCheckoutEvent(this BasketCheckoutDto dto, ShoppingCart basket)
    {
        return new BasketCheckoutEvent
        {
            UserName = dto.UserName,
            TotalPrice = basket.Items.Sum(item => item.Price * item.Quantity),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            EmailAddress = dto.EmailAddress,
            AddressLine = dto.AddressLine,
            Country = dto.Country,
            State = dto.State,
            ZipCode = dto.ZipCode,
            CardName = dto.CardName,
            CardNumber = dto.CardNumber,
            Expiration = dto.Expiration,
            Cvv = dto.Cvv,
            PaymentMethod = dto.PaymentMethod,
            OrderItems = basket.Items.Select(i => new BasketItemEvent
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                ProduceCode = i.ProduceCode,
                Price = i.Price,
                Quantity = i.Quantity,
                ItemWiseDiscount = i.ItemWiseDiscount
            }).ToList()
        };
    }
}
