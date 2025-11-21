

using Catalog.Application.Commands;
using Catalog.Application.Interfaces;
using MediatR;

namespace Catalog.Application.Handlers;

public class DeleteBrandHandler : IRequestHandler<DeleteBrandCommand, bool>
{
    private readonly IBrandRepository _repo;

    public DeleteBrandHandler(IBrandRepository repo) => _repo = repo;

    public async Task<bool> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException("Brand Not Found");

        await _repo.RemoveAsync(entity);    
        await _repo.SaveChangesAsync(cancellationToken);
        return true;
    }
}
