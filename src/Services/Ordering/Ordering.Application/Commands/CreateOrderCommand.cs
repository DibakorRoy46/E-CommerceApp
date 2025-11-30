
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Domain.Enums;

namespace Ordering.Application.Commands;

public sealed record CreateOrderCommand(
    string UserId,
    string? UserName,
    decimal? GrossValue,
    decimal? DiscountValue,
    decimal? NetValue,
    int NumberOfItems,
    string? FirstName,
    string? LastName,
    string? EmailAddress,
    string? AddressLine,
    string? Country,
    string? State,
    string? ZipCode,
    string? CardName,
    string? CardNumber,
    string? Expiration,
    string? Cvv,
    PaymentMethodEnum PaymentMethod,
    OrderStatusEnum Status,
    string Remarks,
    List<OrderItemDto> OrderItems) : IRequest<OrderDto>;