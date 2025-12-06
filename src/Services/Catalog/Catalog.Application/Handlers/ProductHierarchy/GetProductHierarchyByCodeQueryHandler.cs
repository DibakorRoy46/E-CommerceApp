

using AutoMapper;
using Catalog.Application.DTOs;
using Catalog.Application.Interfaces;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetProductHierarchyByCodeQueryHandler :
     IRequestHandler<GetProductHierarchyByCodeQuery, ProductHierarchyResponse>
{
    private readonly IProductHierarchyRepository _repo;
    private readonly IMapper _mapper;

    public GetProductHierarchyByCodeQueryHandler(IProductHierarchyRepository repo,IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ProductHierarchyResponse> Handle(GetProductHierarchyByCodeQuery request, CancellationToken cancellationToken)
    {
        var result=await _repo.GetByCodeAsync(request.Code, cancellationToken);

        if(result==null)
        {
            throw new KeyNotFoundException($"ProductHierarchy with Code {request.Code} not found.");
        }

        return _mapper.Map<ProductHierarchyResponse>(result);
    }
}
