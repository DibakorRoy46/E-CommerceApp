

using Catalog.Application.DTOs;
using Catalog.Application.Interfaces;
using Catalog.Application.Queries;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductHierarchyByCodeQueryHandler :
     IRequestHandler<GetProductHierarchyByCodeQuery,ProductHierarchyDto>
{
    private readonly IProductHierarchyRepository _repo;

    public GetProductHierarchyByCodeQueryHandler(IProductHierarchyRepository repo)=>_repo = repo;

    public async Task<ProductHierarchyDto> Handle(GetProductHierarchyByCodeQuery request, CancellationToken cancellationToken)
    {
        var result=await _repo.GetByCodeAsync(request.Code, cancellationToken);

        if(result==null)
        {
            throw new KeyNotFoundException($"ProductHierarchy with Code {request.Code} not found.");
        }
        return new ProductHierarchyDto(result.Id, result.Name, result.Code, result.LevelId,
            result.ParentId, result.Status, result.CreatedDate, result.CreatedBy,
            result.ModifiedDate, result.ModifiedBy);
    }
}
