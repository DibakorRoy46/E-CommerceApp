
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Repositories;
using Ordering.Application.Specs;
using Ordering.Domain.Entities;
using Ordering.Insfrastrueture.Presistence;

namespace Ordering.Insfrastrueture.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _db;

    public OrderRepository(AppDbContext db)
    {
        _db = db;
    }
    public async Task<bool> AddOrderAsync(Order order)
    {
        var result= await _db.Orders.AddAsync(order);
        return result.State == EntityState.Added;
    }

    public async Task<bool> DeleteOrderAsync(int orderId)
    {
        var entity= await _db.Orders.Include(x=>x.OrderItems).AsNoTracking().FirstOrDefaultAsync(x=>x.OrderId==orderId);
        if (entity == null)
            throw new Exception("Order is not Exist");

        _db.OrderItems.RemoveRange(entity.OrderItems);
        _db.Orders.Remove(entity);
        return true;

    }

    public async Task<List<Order>> GetAllOrdersAsync(OrderSpec orderSpec, CancellationToken cancellationToken = default)
    {
        IQueryable<Order> query = _db.Orders.Include(o => o.OrderItems).AsNoTracking();

        // Apply specification filters

        if (orderSpec.StartDate.HasValue)
            query = query.Where(x => x.CreatedDate >= orderSpec.StartDate.Value);

        if (orderSpec.EndDate.HasValue)
            query = query.Where(x => x.CreatedDate <= orderSpec.EndDate.Value);

        if (orderSpec.OrderStatus.HasValue)
            query = query.Where(x => x.Status == orderSpec.OrderStatus.Value);

        if(orderSpec.PaymentMethod.HasValue)
            query = query.Where(x=>x.PaymentMethod == orderSpec.PaymentMethod.Value);

        return await query.OrderBy(x=>x.CreatedDate).ToListAsync(cancellationToken);
    }

    public async Task<Order?> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken = default)
    {
        var entity = await _db.Orders.Include(x=>x.OrderItems).AsNoTracking().FirstOrDefaultAsync(x => x.OrderId == orderId);
        return entity;
    }

    public async Task<List<Order>> GetOrderByNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        var entities = await _db.Orders.Include(x => x.OrderItems).AsNoTracking().Where(x => x.UserName == userName).ToListAsync(cancellationToken);
        return entities;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> UpdateOrderAsync(Order order)
    {
        _db.Orders.Update(order);
        return await Task.FromResult(true);
    }
}
