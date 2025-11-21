
using AutoMapper;
using Catalog.Application.DTOs;
using Catalog.Application.Interfaces;
using Catalog.Application.Queries;
using MediatR;

namespace Catalog.Application.Handlers;

public class GetBrandByIdHandler : IRequestHandler<GetBrandByIdQuery, BrandDto>
{
    private readonly IBrandRepository _repo;
    private readonly IMapper _mapper;

    public GetBrandByIdHandler(IBrandRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<BrandDto> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repo.GetByIdAsync(request.Id, cancellationToken);

        if (result == null)
        {
            throw new KeyNotFoundException($"Brand with Id {request.Id} not found.");
        }
        return _mapper.Map<BrandDto>(result);
    }
}
