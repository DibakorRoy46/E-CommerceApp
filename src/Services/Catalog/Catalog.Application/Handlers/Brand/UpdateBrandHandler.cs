

using Catalog.Application.Commands;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using MediatR;

namespace Catalog.Application.Handlers;

public class UpdateBrandHandler : IRequestHandler<UpdateBrandCommand, Unit>
{
    private readonly IBrandRepository _repo;

    public UpdateBrandHandler(IBrandRepository repo) => _repo = repo;
    
    public async Task<Unit> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var entity=await _repo.GetByIdAsync(request.Id,cancellationToken)
            ?? throw new KeyNotFoundException("Brand is not found");

        entity.Update(request.Name, request.Code, request.Status, request.ModifiedBy);
        await _repo.UpdateAsync(entity);
        await _repo.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
