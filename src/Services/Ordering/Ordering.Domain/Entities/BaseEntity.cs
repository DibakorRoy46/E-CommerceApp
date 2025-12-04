

namespace Ordering.Domain.Entities;

public class BaseEntity
{
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }

    protected void SetCreated(string createdBy)
    {
        this.CreatedBy = createdBy;
        this.CreatedDate = DateTime.Now;
    }

    protected void SetModified(string modifiedBy) 
    {
        this.ModifiedBy = modifiedBy;
        this.ModifiedDate = DateTime.Now;
    }
}


