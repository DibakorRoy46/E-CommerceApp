using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using MediatR;

namespace Catalog.Application.Handlers;

public class CreateProductHierarchyHandler :IRequestHandler<CreateProductHierarchyCommand, int>
{
    private readonly IProductHierarchyRepository _repo;
    private readonly IMapper _mappper;
    public CreateProductHierarchyHandler(IProductHierarchyRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mappper = mapper;
    }

    public async Task<int> Handle(CreateProductHierarchyCommand request, CancellationToken ct)
    {
        var entity = _mappper.Map<ProductHierarchy>(request);
        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync(ct);
        return entity.Id;
    }
}
