

using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Queries;

public record GetProductByIdQuery( int id ) : IRequest<ProductDto>;
