
using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using MediatR;

namespace Catalog.Application.Handlers;

public class CreateBrandHandler : IRequestHandler<CreateBrandCommand, Unit>
{
    private readonly IBrandRepository _repo;
    private readonly IMapper _mapper;
    public CreateBrandHandler(IBrandRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var entity= _mapper.Map<Brand>(request);
        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
