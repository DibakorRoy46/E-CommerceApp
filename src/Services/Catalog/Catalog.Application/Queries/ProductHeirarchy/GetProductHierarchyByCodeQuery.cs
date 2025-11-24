using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProductHierarchyByCodeQuery : IRequest<ProductHierarchyDto>
{
    public string Code { get; }
    public GetProductHierarchyByCodeQuery(string code)
    {
        Code = code;
    }
}
