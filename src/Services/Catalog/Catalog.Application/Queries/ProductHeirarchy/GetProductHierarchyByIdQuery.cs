

using Catalog.Application.DTOs;
using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProductHierarchyByIdQuery :IRequest<ProductHierarchyResponse>
{
    public int Id { get; }
    public GetProductHierarchyByIdQuery(int id)
    {
        Id = id;
    }
}
