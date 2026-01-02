
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Ordering.Application.Repositories;
using Ordering.Insfrastrueture.Presistence;

namespace Ordering.Insfrastrueture.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(AppDbContext db)
    {
        _db = db;
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken)
    {
        _transaction = await _db.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        if (_transaction != null)
            await _transaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
    {
        if (_transaction != null)
            await _transaction.RollbackAsync(cancellationToken);
    }

    public async Task ExecuteInTransactionAsync(Func<CancellationToken, Task> action,
        CancellationToken cancellationToken)
    {
        var strategy = _db.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                await action(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch(Exception ex)
            {
                throw;
            }
        });
    }
}
