using Catalog.Application.Commands;
using Catalog.Application.Interfaces;
using MediatR;

namespace Catalog.Application.Handlers;


public class UpdateProductHierarchyHandler : IRequestHandler<UpdateProductHierarchyCommand, Unit>
{
    private readonly IProductHierarchyRepository _repo;
    public UpdateProductHierarchyHandler(IProductHierarchyRepository repo)
    => _repo = repo;
    public async Task<Unit> Handle(UpdateProductHierarchyCommand request,
    CancellationToken ct)
    {
        var entity = await _repo.GetByIdAsync(request.Id, ct)
            ?? throw new KeyNotFoundException("ProductHierarchy not found");
        entity.Update(request.Name, request.Code,request.LevelId,request.ParentId, request.Status, request.ModifiedBy);
        _repo.Update(entity);
        await _repo.SaveChangesAsync(ct);
        return Unit.Value;
    }
}
