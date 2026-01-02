
using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Catalog.Application.Handlers;

public class CreateBrandHandler : IRequestHandler<CreateBrandCommand, Unit>
{
    private readonly IBrandRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateBrandHandler> _logger;
    public CreateBrandHandler(IBrandRepository repo, IMapper mapper, ILogger<CreateBrandHandler> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        bool isCodeExists = await _repo.IsCodeExistAsync(request.Code, cancellationToken);

        if(isCodeExists)
        {
            _logger.LogError("Brand with code {Code} already exists.", request.Code);
            throw new ApplicationException($"Brand with code {request.Code} already exists.");
        }

        var entity= _mapper.Map<Brand>(request);
        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
