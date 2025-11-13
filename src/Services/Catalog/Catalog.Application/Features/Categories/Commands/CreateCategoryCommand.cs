
using MediatR;

namespace Catalog.Application.Features.Categories.Commands;

public record CreateCategoryCommand(string Name, string Code, string? Description) : IRequest<Guid>;
