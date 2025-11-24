

using Catalog.Application.Commands;
using FluentValidation;

namespace Catalog.Application.Validators;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.")
            .MinimumLength(3).WithMessage("Product name must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

        RuleFor(x=>x.Code)
            .NotEmpty().WithMessage("Product code is required.")
            .MinimumLength(5).WithMessage("Product code must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Product code must not exceed 50 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Product description is required.")
            .MaximumLength(500).WithMessage("Product description must not exceed 500 characters.");

        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Product price is required.")
            .GreaterThan(0).WithMessage("Product price must be greater than zero.");

        RuleFor(x=>x.BrandId)
            .NotEmpty().WithMessage("Brand Id is required.")
            .GreaterThan(0).WithMessage("Brand Id must be greater than zero.");

        RuleFor(x => x.UnitId)
           .NotEmpty().WithMessage("Unit Id is required.")
           .IsInEnum().WithMessage("Unit Id must be greater than zero.");

        RuleFor(x => x.Weight)
            .NotEmpty().WithMessage("Product weight is required.")
            .GreaterThan(0).WithMessage("Product weight must be greater than zero.");

        RuleFor(x => x.BarCode)
            .NotEmpty().WithMessage("Product barcode is required.")
            .MaximumLength(100).WithMessage("Product barcode must not exceed 100 characters.");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("Product image URL is required.")
            .MaximumLength(200).WithMessage("Product image URL must not exceed 200 characters.");

    }
}
