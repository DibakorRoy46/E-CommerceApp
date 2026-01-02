using Catalog.Application.DTOs;
using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProductHierarchyByCodeQuery : IRequest<ProductHierarchyResponse>
{
    public string Code { get; }
    public GetProductHierarchyByCodeQuery(string code)
    {
        Code = code;
    }
}
