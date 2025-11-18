
using Catalog.Domain.Enums;

namespace Catalog.Domain.Entities;

public class BaseEntity
{
    public int Id { get; protected set; }
    public string Name { get; protected set; }
    public string Code { get; protected set; }
    public StatusEnum Status { get; protected set; }
    public DateTimeOffset CreatedDate { get; protected set; }
    public string? CreatedBy { get; protected set; }
    public DateTimeOffset? ModifiedDate { get; protected set; }
    public string? ModifiedBy { get; protected set; }
    protected void SetCreated(string? createdBy)
    {
        CreatedDate = DateTimeOffset.UtcNow;
        CreatedBy = createdBy;
    }
    protected void SetModified(string? modifiedBy)
    {
        ModifiedDate = DateTimeOffset.UtcNow;
        ModifiedBy = modifiedBy;
    }
}
