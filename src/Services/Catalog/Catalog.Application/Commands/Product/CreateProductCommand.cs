
using Catalog.Domain.Enums;
using MediatR;

namespace Catalog.Application.Commands;

public record CreateProductCommand(
        string Name,
        string Code,
        string Description,
        ProductUnitEnum UnitId,
        decimal Weight,
        string ImageUrl,
        int BrandId,
        string BarCode,
        decimal Price,
        string? CreatedBy
) : IRequest<Unit>;

