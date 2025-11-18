
using Catalog.Domain.Enums;
using MediatR;

namespace Catalog.Application.Commands;

public record UpdateProductHierarchyCommand(int Id, string Name, string Code,ProductHierarchyLevelEnum LevelId, 
                int? ParentId, StatusEnum Status, string? ModifiedBy) : IRequest<Unit>;

