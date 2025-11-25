
namespace Discount.Domain.Entities;

public class Coupon
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Code { get; private set; }
    public string Description { get; private set; }
    public decimal Amount { get; private set; }
    public int IsActive { get; private set; }
    public string? CreatedBy { get; private set; }
    public DateTimeOffset CreatedDate { get; private set; }
    public string? ModifiedBy { get; private set; }
    public DateTimeOffset? ModifiedDate { get; private set; }

    private Coupon()
    {
        
    }

    public Coupon(string name, string code, string description, decimal amount, int isActive,string createdBy)
    {
        Name = name;
        Code = code;
        Description = description;
        Amount = amount;
        IsActive = isActive;
        SetCreated(createdBy);
    }

    public void Update(string name, string code, string description, decimal amount, int isActive, string? modifiedBy)
    {
        Name = name;
        Code = code;
        Description = description;
        Amount = amount;
        IsActive = isActive;
        SetModified(modifiedBy);
    }

    private void SetCreated(string? createdBy)
    {
        CreatedDate = DateTimeOffset.UtcNow;
        CreatedBy = createdBy;
    }
    private void SetModified(string? modifiedBy)
    {
        ModifiedDate = DateTimeOffset.UtcNow;
        ModifiedBy = modifiedBy;
    }
}
