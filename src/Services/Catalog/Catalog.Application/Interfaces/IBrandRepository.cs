
using Catalog.Domain.Entities;
using Catalog.Domain.Enums;

namespace Catalog.Application.Interfaces;

public interface IBrandRepository
{
    Task<List<Brand>> GetAllAsync(StatusEnum? status,CancellationToken cancellationToken=default);
    Task<Brand?> GetByIdAsync(int id, CancellationToken cancellationToken=default);
    Task<Brand?> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<bool> AddAsync(Brand brand);
    Task<bool> UpdateAsync(Brand brand);
    Task<bool> RemoveAsync(Brand brand);
    Task SaveChangesAsync(CancellationToken cancellation=default);
}
