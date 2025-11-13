

using Catalog.Application.Features.Categories.Services;
using Catalog.Domain.Entities;
using MediatR;

namespace Catalog.Application.Features.Categories.Queries;

public class GetCategoriesHandler(ICategoryService categoryService) : IRequestHandler<GetCategoriesQuery, List<Category>>
{
    private readonly ICategoryService _categoryService = categoryService;

    public async Task<List<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _categoryService.GetAllCategoriesAsync(cancellationToken);
    }
}
