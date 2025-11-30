
using AutoMapper;
using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Queries;
using Ordering.Application.Repositories;

namespace Ordering.Application.Handlers;

public class GetOrdersByUserNameHandler : IRequestHandler<GetOrderByUserNameQuery, List<OrderDto>>
{
    private readonly IOrderRepository _repo;
    private readonly IMapper _mapper;

    public GetOrdersByUserNameHandler(IOrderRepository repo, IMapper mapper)
    {
       _repo = repo;   
       _mapper = mapper;
    }

    public async Task<List<OrderDto>> Handle(GetOrderByUserNameQuery request, CancellationToken cancellationToken)
    {
        var orders = await _repo.GetOrderByNameAsync(request.UserName);
        return _mapper.Map<List<OrderDto>>(orders);
    }
}
