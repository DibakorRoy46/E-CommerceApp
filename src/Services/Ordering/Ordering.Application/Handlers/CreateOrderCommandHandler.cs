
using AutoMapper;
using MediatR;
using Ordering.Application.Commands;
using Ordering.Application.DTOs;
using Ordering.Application.Mapping;
using Ordering.Application.Repositories;
using Ordering.Domain.Entities;

namespace Ordering.Application.Handlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly IOrderRepository _repo;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IOrderRepository repo, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _repo = repo;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = _mapper.Map<Order>(request);
        foreach (var item in request.OrderItems)
        {
            orderEntity.AddItem(item.ProductId, item.ProductName, item.ProductCode, item.UnitPrice, item.Quantity, item.ItemWiseDiscount);
        }
       
        await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var createdOrder = await _repo.AddOrderAsync(orderEntity);
            await _repo.SaveChangesAsync(cancellationToken);

            var outboxMessage = OrderMapping.MapOutboMessage(createdOrder, request.CorrelationId);
            var outboxNotificationMessage = OrderMapping.MapNotificationOutboxMessage(createdOrder, request.CorrelationId);
            await _repo.SaveOutboxMessageAsync(outboxMessage);
            await _repo.SaveOutboxMessageAsync(outboxNotificationMessage);
            await _repo.SaveChangesAsync(cancellationToken);

            await _unitOfWork.CommitTransactionAsync(cancellationToken);
            return _mapper.Map<OrderDto>(createdOrder);
        }
        catch(Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}
