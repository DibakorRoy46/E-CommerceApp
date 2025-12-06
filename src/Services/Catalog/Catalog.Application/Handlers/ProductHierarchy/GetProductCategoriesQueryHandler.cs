
using Catalog.Application.Interfaces;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductCategoriesQueryHandler : IRequestHandler<GetProductCategoriesQuery, IReadOnlyList<ProductCategoryResponse>>
{
    private readonly IProductHierarchyRepository _repo;

    public GetProductCategoriesQueryHandler(IProductHierarchyRepository repo)
    {
        _repo = repo;
    }

    public async Task<IReadOnlyList<ProductCategoryResponse>> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
    {
        var result = await _repo.GetProductCategoriesAsync(cancellationToken);
        return result;
    }
}
