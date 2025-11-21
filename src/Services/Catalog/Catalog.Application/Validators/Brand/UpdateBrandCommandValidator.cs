

using Catalog.Application.Commands;
using FluentValidation;

namespace Catalog.Application.Validators;

public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
{
    public UpdateBrandCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required")
                          .MaximumLength(100).WithMessage("Name length less than or equal to 100 characters")
                          .MinimumLength(3).WithMessage("Name length more than or equal to 3 characters");

        RuleFor(x=>x.Code).NotEmpty().WithMessage("Code is Required")
                          .MaximumLength(50).WithMessage("Name length less than or equal to 50 characters")
                          .MinimumLength(5).WithMessage("Code length more than or equal to 5 characters");

        RuleFor(x => x.ModifiedBy)
                  .NotEmpty().WithMessage("Created User is required");
    }
}
