

using AutoMapper;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Exceptions;
using Ordering.Application.Queries;
using Ordering.Application.Repositories;

namespace Ordering.Application.Handlers;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly IOrderRepository _repo;
    private readonly IMapper _mapper;

    public GetOrderByIdHandler(IOrderRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _repo.GetOrderByIdAsync(request.OrderId);
        if(order == null)
            throw new OrderNotFoundException(request.OrderId,request.UserId, string.Empty);

        return _mapper.Map<OrderDto>(order);
    }
}
