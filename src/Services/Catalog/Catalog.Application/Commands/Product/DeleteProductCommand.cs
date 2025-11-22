
using MediatR;

namespace Catalog.Application.Commands;

public record DeleteProductCommand( int Id ):IRequest<bool>;
