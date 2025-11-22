
using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Interfaces;
using MediatR;

namespace Catalog.Application.Handlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProductRepository repo, IMapper mapper)
    {
        _repo=repo;
        _mapper=mapper;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity= await _repo.GetProductByIdAsync(request.Id, cancellationToken);
        if (entity is null)
        {
            throw new KeyNotFoundException($"Product with Id {request.Id} not found.");
        }

        entity.Update(request.Name,request.Code,request.Description,request.UnitId,request.Weight,request.ImageUrl,request.BrandId,
            request.BarCode,request.Status,request.ModifiedBy,request.Price,request.StockQuantity);

        await _repo.UpdateAsync(entity);
        await _repo.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
