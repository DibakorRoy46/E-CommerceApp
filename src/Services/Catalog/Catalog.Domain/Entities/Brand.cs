

using Catalog.Domain.Enums;

namespace Catalog.Domain.Entities;

public class Brand : BaseEntity
{
    public int ProductHierarchyId { get; protected set; }
    public ProductHierarchy? ProductHierarchy { get; protected set; }
    private Brand()
    {
        
    }

    public Brand(string name, string code, int productHierarchyId,  string createdBy)
    {
        Name = name;
        Code = code;
        ProductHierarchyId = productHierarchyId;
        Status = StatusEnum.Initiate;
        SetCreated(createdBy);
    }

    public void Update(string name, string code, int productHierarchyId, StatusEnum stutus, string modifiedBy)
    {
        Name=name;
        Code = code;
        ProductHierarchyId = productHierarchyId;
        Status = stutus;
        SetModified(modifiedBy);
    }
}
