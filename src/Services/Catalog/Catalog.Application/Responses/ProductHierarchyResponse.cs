
using Catalog.Domain.Enums;

namespace Catalog.Application.Responses;

public record ProductHierarchyResponse
(
     int Id,
     string Name,
     string Code,
     string LevelName,
     string ParentName,
     StatusEnum Status
);
