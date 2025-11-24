

using Catalog.Domain.Enums;

namespace Catalog.Application.Requests;

public class ProductHierarchyRequest
{
    public ProductHierarchyLevelEnum? LevelId { get; set; }
    public int? ParentId { get; set; }
    public StatusEnum Status { get; set; }
}
