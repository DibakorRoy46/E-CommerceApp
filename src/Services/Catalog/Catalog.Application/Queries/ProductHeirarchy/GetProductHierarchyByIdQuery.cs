

using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProductHierarchyByIdQuery :IRequest<ProductHierarchyDto>
{
    public int Id { get; }
    public GetProductHierarchyByIdQuery(int id)
    {
        Id = id;
    }
}
