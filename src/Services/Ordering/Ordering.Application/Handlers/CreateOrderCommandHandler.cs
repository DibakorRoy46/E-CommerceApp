
using AutoMapper;
using MediatR;
using Ordering.Application.Commands;
using Ordering.Application.DTOs;
using Ordering.Application.Repositories;
using Ordering.Domain.Entities;

namespace Ordering.Application.Handlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _repo;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IOrderRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = _mapper.Map<Order>(request);
        var createdOrder = await _repo.AddOrderAsync(orderEntity);
        await _repo.SaveChangesAsync(cancellationToken);
        return _mapper.Map<OrderDto>(createdOrder);
    }
}
