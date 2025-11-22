
using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Interfaces;
using MediatR;

namespace Catalog.Application.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Unit>
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        bool isExistCode= await _repo.IsProductCodeUniqueAsync(request.Code, cancellationToken);
        if (isExistCode)
        {
            throw new ApplicationException($"Product code '{request.Code}' already exists.");
        }

        bool isExistName= await _repo.IsProductNameUniqueAsync(request.Name, cancellationToken);
        if (isExistName)
        {
            throw new ApplicationException($"Product name '{request.Name}' already exists.");
        }

        var product = _mapper.Map<Domain.Entities.Product>(request);
        await _repo.AddAsync(product);
        await _repo.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
