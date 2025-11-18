

using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories;

public class ProductHierarchyRepository : IProductHierarchyRepository
{
    private readonly AppDbContext _db;
    public ProductHierarchyRepository(AppDbContext db) => _db = db;

    public async Task<bool> AddAsync(ProductHierarchy entity, CancellationToken ct= default)
    {
        var result=await _db.ProductHierarchies.AddAsync(entity, ct);
        return result.State == EntityState.Added;
    }

    public async Task<ProductHierarchy?> GetByIdAsync(int id,CancellationToken ct = default)
    {
        return await _db.ProductHierarchies.FindAsync(new object[] { id }, ct);
    }
    public async Task<ProductHierarchy?> GetByCodeAsync(string code, CancellationToken ct = default)
    {
        return await _db.ProductHierarchies.FirstOrDefaultAsync( x=>x.Code.ToLower() == code.ToLower(), ct);
    }
    public async Task<List<ProductHierarchy>> GetAllAsync(CancellationToken ct = default)
    {
        return await _db.ProductHierarchies.AsNoTracking().ToListAsync(ct);
    }

    public async Task<bool> Remove(ProductHierarchy entity)
    {
        var result=_db.ProductHierarchies.Remove(entity);
        return await Task.FromResult(result.State == EntityState.Deleted);
    }

    public async Task<bool> Update(ProductHierarchy entity)
    {
        var result=_db.ProductHierarchies.Update(entity);
        return await Task.FromResult(result.State == EntityState.Modified);
    }

    public Task SaveChangesAsync(CancellationToken ct = default) =>  _db.SaveChangesAsync(ct);

}
