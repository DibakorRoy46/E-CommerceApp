

using Catalog.Domain.Entities;
using Catalog.Domain.Enums;

namespace Catalog.Application.Interfaces;

public interface IProductHierarchyRepository
{
    Task<ProductHierarchy?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<ProductHierarchy?> GetByCodeAsync(string code, CancellationToken ct = default);
    Task<List<ProductHierarchy>> GetAllAsync(ProductHierarchyLevelEnum? levelId,int? parentId,StatusEnum status,
        CancellationToken cancellationToken);
    Task<bool> AddAsync(ProductHierarchy entity, CancellationToken ct = default);
    Task<bool> Update(ProductHierarchy entity);
    Task<bool> Remove(ProductHierarchy entity);
    Task SaveChangesAsync(CancellationToken ct = default);
}

