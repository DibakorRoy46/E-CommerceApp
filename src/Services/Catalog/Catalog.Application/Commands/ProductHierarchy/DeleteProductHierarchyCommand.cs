
using MediatR;

namespace Catalog.Application.Commands;

public record DeleteProductHierarchyCommand(int Id) : IRequest<bool>;
