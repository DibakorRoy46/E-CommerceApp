
using Catalog.Domain.Enums;
using MediatR;

namespace Catalog.Application.Commands;

public record UpdateBrandCommand(int Id, string Name, string Code, StatusEnum Status, string ModifiedBy):IRequest<Unit>;
