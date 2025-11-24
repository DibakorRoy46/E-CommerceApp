
using AutoMapper;
using Catalog.Application.DTOs;
using Catalog.Application.Interfaces;
using Catalog.Application.Queries;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductsSearchQueryHandler : IRequestHandler<GetProductsSearchQuery, PaginatedDto<ProductDto>>
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public GetProductsSearchQueryHandler(IProductRepository repo,IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<PaginatedDto<ProductDto>> Handle(GetProductsSearchQuery request, CancellationToken cancellationToken)
    {
        var results = await _repo.SearchProductsAsync(request.ProductSpec, cancellationToken);
        var mappeedList = _mapper.Map<IEnumerable<ProductDto>>(results);

        return new PaginatedDto<ProductDto>(
               request.ProductSpec.PageIndex,
               request.ProductSpec.PageSize,
               mappeedList.Count(),
               mappeedList
           );
    }
}
