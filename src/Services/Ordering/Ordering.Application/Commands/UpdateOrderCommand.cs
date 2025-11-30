
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Domain.Enums;

namespace Ordering.Application.Commands;

public sealed record UpdateOrderCommand(
    int OrderId,
    string UserId,
    string? UserName,
    decimal? GrossValue,
    decimal? DiscountValue,
    decimal? NetValue,
    int NumberOfItems,
    string? CardName,
    string? CardNumber,
    string? Expiration,
    string? Cvv,
    PaymentMethodEnum PaymentMethod,
    OrderStatusEnum Status,
    string Remarks,
    List<OrderItemDto> OrderItems ) : IRequest<OrderDto>;
