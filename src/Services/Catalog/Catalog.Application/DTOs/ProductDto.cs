

namespace Catalog.Application.DTOs;

public record ProductDto
    (
        int Id,
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
        int StockQuantity,
        string ? CreatedBy,
        DateTimeOffset? CreatedDate,
        string? ModifiedBy,
        DateTimeOffset? ModifiedDate
    );
