
using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProductCategoriesQuery():IRequest<IReadOnlyList<ProductCategoryResponse>>;