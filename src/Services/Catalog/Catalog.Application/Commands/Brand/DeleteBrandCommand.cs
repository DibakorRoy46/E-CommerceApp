
using MediatR;

namespace Catalog.Application.Commands;

public record DeleteBrandCommand(int Id):IRequest<bool>;
