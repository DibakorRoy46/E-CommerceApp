
using Catalog.Domain.Enums;
using MediatR;

namespace Catalog.Application.Commands;

public record CreateProductHierarchyCommand(string Name, string Code,ProductHierarchyLevelEnum LevelId,
            int? ParentId,string? CreatedBy) : IRequest<int>;

