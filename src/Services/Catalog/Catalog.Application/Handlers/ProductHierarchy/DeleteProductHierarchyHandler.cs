using Catalog.Application.Commands;
using Catalog.Application.Interfaces;
using MediatR;

namespace Catalog.Application.Handlers;


public class DeleteProductHierarchyHandler : IRequestHandler<DeleteProductHierarchyCommand, bool>
{
    private readonly IProductHierarchyRepository _repo;

    public DeleteProductHierarchyHandler(IProductHierarchyRepository repo)
    {
        _repo = repo;
    }

    public async Task<bool> Handle(DeleteProductHierarchyCommand request, CancellationToken ct)
    {
        // Get the entity
        var entity = await _repo.GetByIdAsync(request.Id, ct)
                                 ?? throw new KeyNotFoundException("ProductHierarchy not found");

        // Remove it
        _repo.Remove(entity);
        await _repo.SaveChangesAsync(ct);

        // Return true to indicate success
        return true;
    }
}
