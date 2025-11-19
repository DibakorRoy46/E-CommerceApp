
using Catalog.Domain.Enums;

namespace Catalog.Application.DTOs;

public record ProductHierarchyDto
    (
        int Id,
        string Name,
        string Code,
        ProductHierarchyLevelEnum LevelId,
        int? ParentId,
        StatusEnum Status,
        DateTimeOffset CreatedDate,
        string? CreatedBy,
        DateTimeOffset? ModifiedDate,
        string? ModifiedBy
    );

