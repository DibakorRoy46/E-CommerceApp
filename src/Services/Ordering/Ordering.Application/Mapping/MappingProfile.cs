

using AutoMapper;
using Ordering.Application.Commands;
using Ordering.Application.DTOs;
using Ordering.Domain.Entities;

namespace Ordering.Application.Mapping;

public class MappingProfile :Profile
{
    public MappingProfile()
    {
        CreateMap<OrderDto, Order>();
        CreateMap<Order, OrderDto>();
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<OrderItemDto, OrderItem>();
        CreateMap<CreateOrderCommand, Order>()
            .ForMember(d => d.OrderItems, o => o.Ignore());
        CreateMap<CreateOrderCommand, OrderDto>();
        CreateMap<OrderItemDto, UpdateOrderCommand>();
        CreateMap<UpdateOrderCommand, OrderItemDto>();
    }
}
