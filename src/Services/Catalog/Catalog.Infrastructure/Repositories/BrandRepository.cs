
using Catalog.Application.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Repositories;

public class BrandRepository : IBrandRepository
{
    private readonly AppDbContext _db;

    public BrandRepository(AppDbContext db ) => _db = db;   

    public async Task<bool> AddAsync(Brand brand)
    {
        var result = await _db.Brands.AddAsync(brand);
        return result.State == EntityState.Added;
    }

    public async Task<List<Brand>> GetAllAsync(StatusEnum? status, CancellationToken cancellationToken = default)
    {
        var brands = _db.Brands.AsNoTracking().Include(x=>x.ProductHierarchy).AsQueryable();

        if(status is not null && status != StatusEnum.NoFilter)
        {
            brands = brands.Where(x => x.Status == status);
        }
        return await brands.ToListAsync(cancellationToken);
    }

    public async Task<Brand?> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        return await _db.Brands.AsNoTracking().Include(x=>x.ProductHierarchy).FirstOrDefaultAsync(x => x.Code.ToLower() == code.ToLower(), cancellationToken);
    }

    public async Task<Brand?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _db.Brands.AsNoTracking().Include(x => x.ProductHierarchy).FirstOrDefaultAsync(x=> x.Id == id, cancellationToken);   
    }

    public async Task<bool> RemoveAsync(Brand brand)
    {
        var result=_db.Brands.Remove(brand);
        return await Task.FromResult( result.State == EntityState.Deleted);
    }

    public async Task SaveChangesAsync(CancellationToken cancellation = default)
    {
        await _db.SaveChangesAsync(cancellation);
    }

    public async Task<bool> UpdateAsync(Brand brand)
    {
        var result= _db.Brands.Update(brand);
        return await Task.FromResult(result.State == EntityState.Modified);
    }
}
