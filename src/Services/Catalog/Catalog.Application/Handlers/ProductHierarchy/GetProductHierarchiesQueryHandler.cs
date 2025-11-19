
using Catalog.Application.DTOs;
using Catalog.Application.Interfaces;
using Catalog.Application.Queries;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductHierarchiesQueryHandler
    : IRequestHandler<GetProductHierarchiesQuery, List<ProductHierarchyDto>>
{
    private readonly IProductHierarchyRepository _repository;

    public GetProductHierarchiesQueryHandler(IProductHierarchyRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ProductHierarchyDto>> Handle(GetProductHierarchiesQuery request, CancellationToken cancellationToken)
    {
        var results = await _repository.GetAllAsync(request.LevelId, request.ParentId, request.Status,cancellationToken);

        return results.Select(x => 
                     new ProductHierarchyDto( x.Id,x.Name, x.Code, x.LevelId,x.ParentId, x.Status, x.CreatedDate,x.CreatedBy,
                     x.ModifiedDate, x.ModifiedBy )
            ).ToList();
    }
}