

using Catalog.Application.Specifications;
using Catalog.Domain.Entities;
using Catalog.Domain.Enums;

namespace Catalog.Application.Interfaces;

public interface IProductRepository
{
    Task<bool> IsProductNameUniqueAsync(string productName,CancellationToken cancellationToken= default);
    Task<bool> IsProductCodeUniqueAsync(string productCode, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> GetProductsAsync(StatusEnum? status, CancellationToken cancellationToken = default);
    Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Product?> GetProductByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> SearchProductsAsync(ProductSpecParams specParams, CancellationToken cancellationToken = default);
    Task<bool> AddAsync(Product product);
    Task<bool> UpdateAsync(Product product);
    Task<bool> RemoveAsync(Product product);
    Task SaveChangesAsync(CancellationToken cancellation = default);
}
