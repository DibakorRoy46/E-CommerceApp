
using AutoMapper;
using Catalog.Application.DTOs;
using Catalog.Application.Interfaces;
using Catalog.Application.Queries;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductHierarchyByIdQueryHandler : IRequestHandler<GetProductHierarchyByIdQuery, ProductHierarchyDto>
{
    private readonly IProductHierarchyRepository _repo;
    private readonly IMapper _mapper;

    public GetProductHierarchyByIdQueryHandler(IProductHierarchyRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ProductHierarchyDto> Handle(GetProductHierarchyByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repo.GetByIdAsync(request.Id, cancellationToken);

        if (result is null)
        {
            throw new KeyNotFoundException($"ProductHierarchy with Id {request.Id} was not found.");
        }
        
        return _mapper.Map<ProductHierarchyDto>(result);
    }
}
