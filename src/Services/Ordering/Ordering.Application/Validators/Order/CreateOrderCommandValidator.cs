
using FluentValidation;
using Ordering.Application.Commands;
using Ordering.Domain.Enums;

namespace Ordering.Application.Validators;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    { 
        RuleFor(c => c).NotNull().WithMessage("Order cannot be null");
        RuleFor(c => c.UserId).NotEmpty().WithMessage("UserId can not be null");
        RuleFor(c => c.UserName).NotEmpty().WithMessage("UserName is required");
        RuleFor(c => c.FirstName).NotEmpty().WithMessage("FirstName is required");
        RuleFor(c => c.LastName).NotEmpty().WithMessage("LastName is required");
        RuleFor(c => c.EmailAddress).NotEmpty().WithMessage("EmailAddress is required").EmailAddress().WithMessage("EmailAddress is not valid");
        RuleFor(c => c.AddressLine).NotEmpty().WithMessage("AddressLine is required");
        RuleFor(c => c.Country).NotEmpty().WithMessage("Country is required");
        RuleFor(c => c.OrderItems.Count).GreaterThan(0).WithMessage("Order must have at least one item");
        RuleFor(c => c.OrderItems.Any(x => x.Quantity <= 0)).Equal(false).WithMessage("Order items must have quantity greater than zero");
        RuleFor(c => c.OrderItems.Any(x => x.UnitPrice <= 0)).Equal(false).WithMessage("Order items must have quantity greater than zero");
    }
}
