
using Catalog.Domain.Enums;

namespace Catalog.Domain.Entities;

public class ProductHierarchy : BaseEntity
{
    public ProductHierarchyLevelEnum LevelId { get; private set; }
    public int? ParentId { get; private set; }
    // Factory + behavior
    private ProductHierarchy() { }
    public ProductHierarchy(string name, string code, ProductHierarchyLevelEnum levelId, int? parentId, string? createdBy)
    {
        Name = name;
        Code = code;
        LevelId = levelId;
        ParentId = parentId;
        Status = StatusEnum.Initiate;
        SetCreated(createdBy);
    }
    public void Update(string name, string code, ProductHierarchyLevelEnum levelId, int? parentId, StatusEnum status, string? modifiedBy)
    {
        Name = name;
        Code = code;
        LevelId = levelId;
        ParentId = parentId;
        Status = status;
        SetModified(modifiedBy);
    }
}
