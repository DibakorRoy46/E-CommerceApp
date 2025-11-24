
using AutoMapper;
using Catalog.Application.DTOs;
using Catalog.Application.Interfaces;
using Catalog.Application.Queries;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductHierarchiesQueryHandler
    : IRequestHandler<GetProductHierarchiesQuery, List<ProductHierarchyDto>>
{
    private readonly IProductHierarchyRepository _repository;
    private readonly IMapper _mapper;

    public GetProductHierarchiesQueryHandler(IProductHierarchyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<ProductHierarchyDto>> Handle(GetProductHierarchiesQuery request, CancellationToken cancellationToken)
    {
        var results = await _repository.GetAllAsync(request.LevelId, request.ParentId, request.Status,cancellationToken);
        return _mapper.Map<List<ProductHierarchyDto>>(results);
    }
}