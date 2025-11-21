

using AutoMapper;
using Catalog.Application.DTOs;
using Catalog.Application.Interfaces;
using Catalog.Application.Queries;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetBrandByCodeHandler : IRequestHandler<GetBrandByCodeQuery, BrandDto>
{
    private readonly IBrandRepository _repo;
    private readonly IMapper _mapper;

    public GetBrandByCodeHandler(IBrandRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<BrandDto> Handle(GetBrandByCodeQuery request, CancellationToken cancellationToken)
    {
        var result = await _repo.GetByCodeAsync(request.Code, cancellationToken)
            ?? throw new KeyNotFoundException($"Brand with code {request.Code} is not found");

        return _mapper.Map<BrandDto>(result);
    }
}
