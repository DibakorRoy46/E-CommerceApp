

using Catalog.Application.DTOs;
using Catalog.Application.Responses;
using Catalog.Domain.Enums;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProductHierarchiesQuery : IRequest<List<ProductHierarchyResponse>>
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
