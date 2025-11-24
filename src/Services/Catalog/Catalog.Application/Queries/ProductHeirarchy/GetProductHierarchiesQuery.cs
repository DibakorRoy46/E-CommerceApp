

using Catalog.Application.DTOs;
using Catalog.Domain.Enums;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProductHierarchiesQuery : IRequest<List<ProductHierarchyDto>>
{
    public ProductHierarchyLevelEnum? LevelId { get; }
    public int? ParentId { get; }
    public StatusEnum Status { get; }
    public GetProductHierarchiesQuery(ProductHierarchyLevelEnum? levelId, int? parentId, StatusEnum status)
    {
        LevelId = levelId;
        ParentId = parentId;
        Status = status;
    }
}
