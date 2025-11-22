using Catalog.Application.DTOs;
using Catalog.Application.Specifications;
using Catalog.Domain.Entities; 
using Catalog.Domain.Enums;
using MediatR;

namespace Catalog.Application.Queries;

public record GetProductsSearchQuery(ProductSpecParams ProductSpec) : IRequest<PaginatedDto<ProductDto>>;
