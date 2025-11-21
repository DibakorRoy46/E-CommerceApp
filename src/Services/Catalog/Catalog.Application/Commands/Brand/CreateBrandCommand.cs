
using Catalog.Domain.Enums;
using MediatR;

namespace Catalog.Application.Commands;

public record CreateBrandCommand(string Name,string Code,StatusEnum Status,string CreatedBy):IRequest<Unit>;

