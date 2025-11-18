
using Catalog.Domain.Enums;

namespace Catalog.Application.DTOs;

public record ProductHierarchyDto
    (
        int Id,
        string Name,
        string Code,
        ProductHierarchyLevelEnum levelId,
        int? parentId,
        StatusEnum Status,
        DateTimeOffset CreatedDate,
        string? CreatedBy,
        DateTimeOffset? ModifiedDate,
        string? ModifiedBy
    );

