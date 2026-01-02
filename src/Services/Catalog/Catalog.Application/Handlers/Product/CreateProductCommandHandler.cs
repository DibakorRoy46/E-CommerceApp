
using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using Logging.Abstractions;
using MediatR;

namespace Catalog.Application.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Unit>
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreateProductCommandHandler> _logger;

    public CreateProductCommandHandler(IProductRepository repo, IMapper mapper, IAppLogger<CreateProductCommandHandler> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Creating product {request.Name}");

        bool isExistCode= await _repo.IsProductCodeUniqueAsync(request.Code, cancellationToken);
        if (isExistCode)
        {
            _logger.LogWarning($"Product code '{request.Code}' already exists.");
            throw new ApplicationException($"Product code '{request.Code}' already exists.");
        }

        bool isExistName= await _repo.IsProductNameUniqueAsync(request.Name, cancellationToken);
        if (isExistName)
        {
            _logger.LogWarning($"Product Name '{request.Name}' already exists.");
            throw new ApplicationException($"Product name '{request.Name}' already exists.");
        }

        var product = _mapper.Map<Product>(request);
        await _repo.AddAsync(product);
        await _repo.SaveChangesAsync(cancellationToken);
        _logger.LogInformation($"Product created with Id {product.Id}");
        return Unit.Value;
    }
}
