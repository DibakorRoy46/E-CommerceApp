
using Basket.Application.DTOs;
using MediatR;

namespace Basket.Application.Commands;

public record CheckoutBasketCommand(BasketCheckoutDto checkoutDto): IRequest<Unit>;
