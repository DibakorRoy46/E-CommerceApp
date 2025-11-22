
using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Queries;

public record GetProductByCodeQuery( string code ) : IRequest<ProductDto>;