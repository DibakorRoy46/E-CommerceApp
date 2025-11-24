

using Catalog.Application.DTOs;
using Catalog.Application.Interfaces;
using Catalog.Application.Specifications;
using Catalog.Domain.Entities;
using Catalog.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Catalog.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;
    public ProductRepository(AppDbContext db) => _db = db;

    public async Task<bool> AddAsync(Product product)
    {
        var result= await _db.Products.AddAsync(product);
        return result.State == EntityState.Added;
    }

    public async Task<Product?> GetProductByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var result = await _db.Products.AsNoTracking().Include(x=>x.Brand)
                                        .ThenInclude(b=>b.ProductHierarchy)
                                       .FirstOrDefaultAsync(p => p.Code == code, cancellationToken);
        return result;
    }

    public Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = _db.Products.AsNoTracking().Include(x => x.Brand)
                                       .ThenInclude(b => b.ProductHierarchy)
                                       .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        return result;
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync(StatusEnum? status, CancellationToken cancellationToken = default)
    {
        var result = await _db.Products.AsNoTracking().Include(x => x.Brand)
                                       .ThenInclude(b => b.ProductHierarchy)
                                       .Where(p => !status.HasValue || p.Status == status.Value)
                                       .ToListAsync(cancellationToken);
        return result;
    }

    public async Task<bool> IsProductCodeUniqueAsync(string productCode, CancellationToken cancellationToken = default)
    {
        return await _db.Products.AsNoTracking()
                           .AnyAsync(p => p.Code.ToLower() == productCode.ToLower(), cancellationToken);
    }

    public async Task<bool> IsProductNameUniqueAsync(string productName, CancellationToken cancellationToken = default)
    {
        return await _db.Products.AsNoTracking()
                           .AnyAsync(p => p.Name.ToLower() == productName.ToLower(), cancellationToken);
    }

    public async Task<bool> RemoveAsync(Product product)
    {
        var entity= await _db.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
        if (entity == null)
            throw new KeyNotFoundException("Product is not found.");
        var result = _db.Products.Remove(entity);

        return result.State == EntityState.Deleted;
    }

    public async Task SaveChangesAsync(CancellationToken cancellation = default)
    {
        await _db.SaveChangesAsync(cancellation);
    }

    public async Task<IReadOnlyList<Product>> SearchProductsAsync(ProductSpecParams specParams, CancellationToken cancellationToken = default)
    {
        var query= _db.Products.AsNoTracking().Include(x => x.Brand)
                                       .ThenInclude(b => b.ProductHierarchy)
                                       .AsQueryable();

        if(specParams.Status.HasValue)
        {
            query=query.Where(p=>p.Status==specParams.Status.Value);
        }

        if(specParams.IsInStock.HasValue && specParams.IsInStock == true)
        {
            query=query.Where(p=>p.StockQuantity>0);
        }

        if(specParams.BrandId.HasValue)
        {
            query=query.Where(p=>p.BrandId==specParams.BrandId.Value);
        }

        if(specParams.MinPrice.HasValue)
        {
            query=query.Where(p=>p.Price>=specParams.MinPrice.Value);
        }

        if(specParams.MaxPrice.HasValue)
        {
            query=query.Where(p=>p.Price<=specParams.MaxPrice.Value);
        }

        if(specParams.ProductHierarchyId.HasValue)
        {
            query=query.Where(p=>p.Brand!=null && p.Brand.ProductHierarchyId == specParams.ProductHierarchyId.Value);
        }

        if (!string.IsNullOrEmpty(specParams.SearchTerm))
        {
            var lowerSearchTerm=specParams.SearchTerm.ToLower();
            query=query.Where(p=>p.Name.ToLower()
                             .Contains(lowerSearchTerm) || p.Code.ToLower().Contains(lowerSearchTerm) || p.Description.ToLower()
                             .Contains(lowerSearchTerm));
        }

        // Sorting

        query = specParams.SortBy switch
        {
            SortByEnum.PriceAsc => query.OrderBy(p => p.Price),
            SortByEnum.PriceDesc => query.OrderByDescending(p => p.Price),
            SortByEnum.NameAsc => query.OrderBy(p => p.Name),
            SortByEnum.NameDesc => query.OrderByDescending(p => p.Name),
            _ => query.OrderBy(p => p.Name),
        };

        // Pagination

        var skip = (specParams.PageIndex - 1) * specParams.PageSize;
        query = query.Skip(skip).Take(specParams.PageSize);

        //Execute SQL
        return await query.ToListAsync(cancellationToken);
    }

    public async Task<bool> UpdateAsync(Product product)
    {
       var entity= await _db.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
        if (entity == null)
            throw new KeyNotFoundException("Product is not found.");

        var result = _db.Products.Update(entity);
        return result.State == EntityState.Modified;
    }
}
