
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Domain.Enums;

namespace Ordering.Application.Commands;

public sealed record CreateOrderCommand : IRequest<OrderDto>
{
    public string? UserId { get; init; }
    public string? UserName { get; init; }
    public decimal? GrossValue { get; init; }
    public decimal? DiscountValue { get; init; }
    public decimal? NetValue { get; init; }
    public int NumberOfItems { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? EmailAddress { get; init; }
    public string? AddressLine { get; init; }
    public string? Country { get; init; }
    public string? State { get; init; }
    public string? ZipCode { get; init; }
    public string? CardName { get; init; }
    public string? CardNumber { get; init; }
    public string? Expiration { get; init; }
    public string? Cvv { get; init; }
    public PaymentMethodEnum PaymentMethod { get; init; }
    public OrderStatusEnum Status { get; init; }
    public string Remarks { get; init; }
    public List<OrderItemDto> OrderItems { get; init; } = new();
}
