
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
}
