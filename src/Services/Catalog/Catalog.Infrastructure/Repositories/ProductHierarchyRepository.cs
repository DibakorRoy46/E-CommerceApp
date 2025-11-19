

using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Domain.Enums;
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
    public async Task<List<ProductHierarchy>> GetAllAsync(ProductHierarchyLevelEnum? levelId,int? parentId,StatusEnum status,CancellationToken cancellationToken)
    {
        var query = _db.ProductHierarchies.AsQueryable();

        if (levelId.HasValue)
            query = query.Where(x => x.LevelId == levelId);

        if (parentId.HasValue)
            query = query.Where(x => x.ParentId == parentId);

        if (StatusEnum.NoFilter != status)
            query = query.Where(x => x.Status == status);

        return await query.ToListAsync(cancellationToken);
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
