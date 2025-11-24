
using AutoMapper;
using Catalog.Application.DTOs;
using Catalog.Application.Interfaces;
using Catalog.Application.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net.NetworkInformation;

namespace Catalog.Application.Handlers;

public class GetBrandsHandler : IRequestHandler<GetBrandsQuery, List<BrandDto>>
{
    private readonly IBrandRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<GetBrandsHandler> _logger;
    public GetBrandsHandler(IBrandRepository repo, IMapper mapper, ILogger<GetBrandsHandler> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<BrandDto>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching products with filters {@Filters}", request);

        var brandList = await _repo.GetAllAsync(request.Status, cancellationToken);

        _logger.LogInformation("Fetched {Count} products", brandList.Count());
        return _mapper.Map<List<BrandDto>>(brandList);
    }
}
