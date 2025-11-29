
using AutoMapper;
using MediatR;
using Ordering.Application.Commands;
using Ordering.Application.DTOs;
using Ordering.Application.Repositories;
using Ordering.Domain.Entities;

namespace Ordering.Application.Handlers;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _repo;
    private readonly IMapper _mapper;

    public UpdateOrderCommandHandler(IOrderRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<OrderDto> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = _mapper.Map<Order>(request.Order);

        foreach (var item in request.Order.OrderItems)
        {
            var existingItem = orderEntity.OrderItems
                .FirstOrDefault(oi => oi.ProductId == item.ProductId);
            if (existingItem != null)
            {
                orderEntity.UpdateItem(item.ProductId, item.Quantity, item.ItemWiseDicount);
            }
        }

        var updatedOrder = await _repo.UpdateOrderAsync(orderEntity);
        await _repo.SaveChangesAsync(cancellationToken);
        return _mapper.Map<OrderDto>(updatedOrder);
    }
}
