
using Catalog.Domain.Enums;

namespace Catalog.Application.DTOs;

public record BrandDto( 
    int Id,
    string Name,
    string Code, 
    int ProductHierarchyId,
    string ProductHierarchyName,
    StatusEnum Status,
    string? CreatedBy,
    DateTimeOffset? CreatedDate,                      
    string? ModifiedBy, 
    DateTimeOffset? ModifiedDate
);
