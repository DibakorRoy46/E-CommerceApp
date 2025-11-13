
using Catalog.Application.Common.Interfaces;
using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Application.Features.Categories.Services;

public class CategoryService(IApplicationDbContext dbContext) : ICategoryService
{
    private readonly IApplicationDbContext _dbContext = dbContext;

    public async Task<Category> CreateCategoryAsync(string name, string code, string? description, CancellationToken cancellationToken = default)
    {
        var category = Category.Create(name, code, description);
        _dbContext.Categories.Add(category);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return category;
    }

    public async Task<List<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Categories.ToListAsync(cancellationToken);
    }
}
