

using Catalog.Domain.Entities;
using MediatR;

namespace Catalog.Application.Features.Categories.Queries;

public record GetCategoriesQuery() : IRequest<List<Category>>;
