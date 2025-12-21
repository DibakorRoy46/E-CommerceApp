using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers;

public class CreateProductHierarchyHandler :IRequestHandler<CreateProductHierarchyCommand, int>
{
    private readonly IProductHierarchyRepository _repo;
    private readonly IMapper _mappper;
    private readonly ILogger<IProductHierarchyRepository> _logger;
    public CreateProductHierarchyHandler(IProductHierarchyRepository repo, IMapper mapper,
        ILogger<IProductHierarchyRepository> logger)
    {
        _repo = repo;
        _mappper = mapper;
        _logger = logger;
    }

    public async Task<int> Handle(CreateProductHierarchyCommand request, CancellationToken ct)
    {
        var isExistCode= await _repo.IsCodeExistAsync(request.Code, ct);
        if (isExistCode)
        {
            _logger.LogError("ProductHierarchy with Code: {Code} already exists.", request.Code);
            throw new ApplicationException($"ProductHierarchy with Code: {request.Code} already exists.");
        }

        var entity = _mappper.Map<ProductHierarchy>(request);
        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync(ct);
        return entity.Id;
    }
}
