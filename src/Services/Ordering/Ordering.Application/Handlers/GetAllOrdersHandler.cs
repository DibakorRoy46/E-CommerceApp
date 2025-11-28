
using AutoMapper;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Queries;
using Ordering.Application.Repositories;

namespace Ordering.Application.Handlers;

public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _repo;
    private readonly IMapper _mapper;

    public GetAllOrdersHandler(IOrderRepository repo , IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await _repo.GetAllOrdersAsync(request.OrderSpec, cancellationToken);
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }
}
