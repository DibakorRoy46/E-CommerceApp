
using AutoMapper;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Queries;
using Ordering.Application.Repositories;

namespace Ordering.Application.Handlers;

public class GetOrdersByUserNameHandler : IRequestHandler<GetOrderByUserNameQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _repo;
    private readonly IMapper _mapper;

    public GetOrdersByUserNameHandler(IOrderRepository repo, IMapper mapper)
    {
       _repo = repo;   
       _mapper = mapper;
    }
    public async Task<IEnumerable<OrderDto>> Handle(GetOrderByUserNameQuery request, CancellationToken cancellationToken)
    {
        var orders = await _repo.GetOrderByNameAsync(request.UserName);
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }
}
