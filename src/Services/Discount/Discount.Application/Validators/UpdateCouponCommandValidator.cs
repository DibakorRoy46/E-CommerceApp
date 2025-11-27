

using Discount.Application.Commands;
using FluentValidation;

namespace Discount.Application.Validators;

public class UpdateCouponCommandValidator : AbstractValidator<UpdateCouponCommand>
{
    public UpdateCouponCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Name is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(100).WithMessage("Name maximum length is equal or less than 100 characters ")
            .MinimumLength(5).WithMessage("Name minimum length is greater or equal to 5 characters");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required")
            .MaximumLength(50).WithMessage("Code maximum length is equal or less than 50 characters ")
            .MinimumLength(5).WithMessage("Code minimum length is greater or equal to 5 characters");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required")
            .MaximumLength(500).WithMessage("Description maximum length is equal or less than 500 characters ")
            .MinimumLength(20).WithMessage("Description minimum length is greater or equal to 20 characters");

        RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("Description is required")
            .GreaterThan(0).WithMessage("Amount will be greater than zero");

        RuleFor(x => x.IsActive)
            .InclusiveBetween(0, 1)
            .WithMessage("IsActive must be either 0 (inactive) or 1 (active).");

        RuleFor(x => x.ModifiedBy)
            .NotEmpty().WithMessage("ModifiedBy is required");
    }
}
