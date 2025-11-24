
using AutoMapper;
using Catalog.Application.DTOs;
using Catalog.Application.Interfaces;
using Catalog.Application.Queries;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductByCodeQueryHandler : IRequestHandler<GetProductByCodeQuery, ProductDto>
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public GetProductByCodeQueryHandler(IProductRepository repo,IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductByCodeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repo.GetProductByCodeAsync(request.code,cancellationToken);
        return _mapper.Map<ProductDto>(result);
    }
}
