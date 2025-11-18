using Catalog.Application.Commands;
using Catalog.Application.Handlers;
using Catalog.Domain.Enums;
using FluentValidation;

namespace Catalog.Application.Validators;

public class CreateProductHierarchyCommandValidator : AbstractValidator<CreateProductHierarchyCommand>
{
    public CreateProductHierarchyCommandValidator()
    {
        RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Name is required")
                    .MaximumLength(200).MinimumLength(3);
        RuleFor(x => x.Code)
                    .NotEmpty().WithMessage("Code is required")
                    .MaximumLength(50).MinimumLength(5);
        RuleFor(x => x.LevelId)
                    .Must(value => Enum.IsDefined(typeof(ProductHierarchyLevelEnum), value))
                    .WithMessage("LevelId is invalid.");
        RuleFor(x => x.CreatedBy)
                    .NotEmpty().WithMessage("Name is required");

    }
}

