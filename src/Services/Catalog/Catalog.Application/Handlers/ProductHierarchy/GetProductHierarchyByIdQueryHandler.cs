
using Catalog.Application.DTOs;
using Catalog.Application.Interfaces;
using Catalog.Application.Queries;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductHierarchyByIdQueryHandler : IRequestHandler<GetProductHierarchyByIdQuery, ProductHierarchyDto>
{
    private readonly IProductHierarchyRepository _repo;

    public GetProductHierarchyByIdQueryHandler(IProductHierarchyRepository repo) =>_repo = repo;
    public async Task<ProductHierarchyDto> Handle(GetProductHierarchyByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repo.GetByIdAsync(request.Id, cancellationToken);

        if (result is null)
        {
            throw new KeyNotFoundException($"ProductHierarchy with Id {request.Id} was not found.");
        }

        return new ProductHierarchyDto(result.Id, result.Name, result.Code, result.LevelId, result.ParentId, result.Status, 
            result.CreatedDate, result.CreatedBy, result.ModifiedDate, result.ModifiedBy);
    }
}
