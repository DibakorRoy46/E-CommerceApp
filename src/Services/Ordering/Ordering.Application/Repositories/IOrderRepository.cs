
using Ordering.Application.Specs;
using Ordering.Domain.Entities;

namespace Ordering.Application.Repositories;

public interface IOrderRepository
{
    Task<bool> AddOrderAsync(Order order);
    Task<bool> UpdateOrderAsync(Order order);
    Task<bool> DeleteOrderAsync(int orderId);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<Order?> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken = default);
    Task<List<Order>> GetOrderByNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<List<Order>> GetAllOrdersAsync(OrderSpec orderSpec, CancellationToken cancellationToken = default);
}
