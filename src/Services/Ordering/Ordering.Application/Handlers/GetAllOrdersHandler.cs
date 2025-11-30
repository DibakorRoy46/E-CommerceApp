
using AutoMapper;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Queries;
using Ordering.Application.Repositories;

namespace Ordering.Application.Handlers;

public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, List<OrderDto>>
{
    private readonly IOrderRepository _repo;
    private readonly IMapper _mapper;

    public GetAllOrdersHandler(IOrderRepository repo , IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<List<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _repo.GetAllOrdersAsync(request.OrderSpec, cancellationToken);
        return _mapper.Map<List<OrderDto>>(orders);
    }
}
