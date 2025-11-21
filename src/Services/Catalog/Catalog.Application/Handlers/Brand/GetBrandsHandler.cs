
using AutoMapper;
using Catalog.Application.DTOs;
using Catalog.Application.Interfaces;
using Catalog.Application.Queries;
using MediatR;
using System.Linq;

namespace Catalog.Application.Handlers;

public class GetBrandsHandler : IRequestHandler<GetBrandsQuery, List<BrandDto>>
{
    private readonly IBrandRepository _repo;
    private readonly IMapper _mapper;
    public GetBrandsHandler(IBrandRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<List<BrandDto>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
    {
        var brandList= await _repo.GetAllAsync(request.Status, cancellationToken);
        return _mapper.Map<List<BrandDto>>(brandList);
    }
}
