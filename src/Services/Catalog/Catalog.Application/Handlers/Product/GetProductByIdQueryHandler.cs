
using AutoMapper;
using Catalog.Application.DTOs;
using Catalog.Application.Interfaces;
using Catalog.Application.Queries;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;
    
    public GetProductByIdQueryHandler(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repo.GetProductByIdAsync(request.id, cancellationToken);
        return _mapper.Map<ProductDto>(result);
    }
}
