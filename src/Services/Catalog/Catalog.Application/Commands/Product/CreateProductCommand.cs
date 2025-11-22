
using MediatR;

namespace Catalog.Application.Commands;

public record CreateProductCommand(
        string Name,
        string Code,
        string Description,
        int UnitId,
        decimal Weight,
        string ImageUrl,
        int BrandId,
        string BrandName,
        string BarCode,
        decimal Price,
        string? CreatedBy
) : IRequest<Unit>;

