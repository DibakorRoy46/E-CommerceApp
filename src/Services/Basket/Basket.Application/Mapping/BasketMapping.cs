

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
            UserId = dto.UserName,
            UserName = dto.UserName,
            GrossValue = basket.Items.Sum(item => item.Price * item.Quantity),
            DiscountValue = basket.Discount,
            NetValue = basket.Items.Sum(item => item.Price * item.Quantity) - basket.Discount,
            NumberOfItems = basket.Items.Count,
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
            Remarks = dto.Remarks,
            OrderItems = basket.Items.Select(i => new BasketItemEvent
            {
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                ProductCode = i.ProductCode,
                UnitPrice = i.Price,
                Quantity = i.Quantity,
                ItemWiseDiscount = i.ItemWiseDiscount
            }).ToList()
        };
    }
}
