

using Catalog.Application.Features.Categories.Services;
using MediatR;

namespace Catalog.Application.Features.Categories.Commands;

public class CreateCategoryHandler(ICategoryService categoryService) : IRequestHandler<CreateCategoryCommand, Guid>
{
    private readonly ICategoryService _categoryService = categoryService;

    public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryService.CreateCategoryAsync(request.Name ,request.Code, request.Description, cancellationToken);
        return category.Id;
    }
}
