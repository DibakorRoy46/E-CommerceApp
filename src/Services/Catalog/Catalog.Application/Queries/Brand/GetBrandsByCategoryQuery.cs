
using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public record GetBrandsByCategoryQuery(int? CategoryId): IRequest<IReadOnlyList<BrandResponse>>;
