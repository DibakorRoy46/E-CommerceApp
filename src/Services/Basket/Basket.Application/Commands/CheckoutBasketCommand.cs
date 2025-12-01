
using Basket.Application.DTOs;
using MediatR;

namespace Basket.Application.Commands;

public record CheckoutBasketCommand(
        string UserName,
        decimal TotalPrice,
        string FirstName,
        string LastName,
        string EmailAddress,
        string AddressLine,
        string Country,
        string State,
        string ZipCode,
        string CardName,
        string CardNumber,
        string Expiration,
        string Cvv,
        int PaymentMethod,
        string Remarks
    ) : IRequest<Unit>;
