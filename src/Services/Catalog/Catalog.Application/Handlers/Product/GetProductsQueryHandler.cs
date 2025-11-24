

using AutoMapper;
using Catalog.Application.DTOs;
using Catalog.Application.Interfaces;
using Catalog.Application.Queries;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductDto>>
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public GetProductsQueryHandler(IProductRepository repo,IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<List<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var results = await _repo.GetProductsAsync(request.Status, cancellationToken);
        return _mapper.Map<List<ProductDto>>(results);
    }


}
