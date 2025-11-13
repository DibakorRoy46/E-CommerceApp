using Catalog.Domain.Entities;

namespace Catalog.Application.Features.Categories.Services;

public interface ICategoryService
{
    Task<Category> CreateCategoryAsync(string name, string code, string? description, CancellationToken cancellationToken = default);
    Task<List<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken = default);
}
