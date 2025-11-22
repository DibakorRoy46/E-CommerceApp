

using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Interfaces;
using MediatR;

namespace Catalog.Application.Handlers;

internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;
    public DeleteProductCommandHandler(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;   
    }
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetProductByIdAsync(request.Id,cancellationToken);
        if (entity is null)
        {
            throw new KeyNotFoundException($"Product with Id {request.Id} not found.");
        }

        await _repo.RemoveAsync(entity);
        await _repo.SaveChangesAsync(cancellationToken);
        return true;
    }
}
