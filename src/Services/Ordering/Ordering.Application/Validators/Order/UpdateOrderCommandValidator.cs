
using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommnad>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(c => c.Order).NotNull().WithMessage("Order cannot be null");
        RuleFor(c => c.Order.OrderId).GreaterThan(0).WithMessage("Order Id must be greater than zero");
        RuleFor(c => c.Order.UserId).NotEmpty().WithMessage("UserId can not be null");
        RuleFor(c => c.Order.UserName).NotEmpty().WithMessage("UserName is required");
        RuleFor(c => c.Order.FirstName).NotEmpty().WithMessage("FirstName is required");
        RuleFor(c => c.Order.LastName).NotEmpty().WithMessage("LastName is required");
        RuleFor(c => c.Order.EmailAddress).NotEmpty().WithMessage("EmailAddress is required").EmailAddress().WithMessage("EmailAddress is not valid");
        RuleFor(c => c.Order.AddressLine).NotEmpty().WithMessage("AddressLine is required");
        RuleFor(c => c.Order.Country).NotEmpty().WithMessage("Country is required");
        RuleFor(c => c.Order.OrderItems.Count).GreaterThan(0).WithMessage("Order must have at least one item");
        RuleFor(c=> c.Order.OrderItems.Any(x=>x.Quantity <=0)).Equal(false).WithMessage("Order items must have quantity greater than zero");
        RuleFor(c=> c.Order.OrderItems.Any(x=>x.UnitPrice <=0)).Equal(false).WithMessage("Order items must have quantity greater than zero");
    }
}
