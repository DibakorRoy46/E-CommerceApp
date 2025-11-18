using Catalog.Application.Commands;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using MediatR;

namespace Catalog.Application.Handlers;

public class CreateProductHierarchyHandler :IRequestHandler<CreateProductHierarchyCommand, int>
{
    private readonly IProductHierarchyRepository _repo;
    public CreateProductHierarchyHandler(IProductHierarchyRepository repo)
    => _repo = repo;
    public async Task<int> Handle(CreateProductHierarchyCommand request, CancellationToken ct)
    {
        var entity = new ProductHierarchy(request.Name, request.Code, request.LevelId, request.ParentId, request.CreatedBy);
        await _repo.AddAsync(entity, ct);
        await _repo.SaveChangesAsync(ct);
        return entity.Id;
    }
}
