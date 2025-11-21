

using Catalog.Domain.Enums;

namespace Catalog.Domain.Entities;

public class Brand : BaseEntity
{
    private Brand()
    {
            
    }

    public Brand(string name, string code,  string createdBy)
    {
        Name = name;
        Code = code;
        Status = StatusEnum.Initiate;
        SetCreated(createdBy);
    }

    public void Update(string name, string code, StatusEnum stutus, string modifiedBy)
    {
        Name=name;
        Code = code;
        Status = stutus;
        SetModified(modifiedBy);
    }
}
