
using Catalog.Domain.Enums;
using MediatR;

namespace Catalog.Application.Commands;

public record UpdateProductCommand
    (
        int Id,
        string Name,
        string Code,
        string Description,
        ProductUnitEnum UnitId,
        decimal Weight,
        string ImageUrl,
        int BrandId,
        string BrandName,
        string BarCode,
        decimal Price,
        int StockQuantity,
        string ModifiedBy,
        StatusEnum Status
    ):IRequest<Unit>;