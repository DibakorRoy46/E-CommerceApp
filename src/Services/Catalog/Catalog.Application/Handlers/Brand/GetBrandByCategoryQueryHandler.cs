
using Catalog.Application.Interfaces;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetBrandByCategoryQueryHandler : IRequestHandler<GetBrandsByCategoryQuery, IReadOnlyList<BrandResponse>>
{
    private readonly IBrandRepository _repo;

    public GetBrandByCategoryQueryHandler(IBrandRepository repo)
    {
        _repo = repo;
    }
    public async Task<IReadOnlyList<BrandResponse>> Handle(GetBrandsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var brands = await _repo.GetBrandsByCategoryAsync(request.CategoryId, cancellationToken);
        return brands;
    }
}
