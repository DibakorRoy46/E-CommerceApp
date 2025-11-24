
using Catalog.Application.Commands;
using Catalog.Domain.Enums;
using FluentValidation;

namespace Catalog.Application.Validators;

public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Name is required")
                    .MaximumLength(200).MinimumLength(3);
        RuleFor(x => x.Code)
                    .NotEmpty().WithMessage("Code is required")
                    .MaximumLength(50).MinimumLength(5);
        RuleFor(x => x.CreatedBy)
                    .NotEmpty().WithMessage("Created User is required");

    }
}
