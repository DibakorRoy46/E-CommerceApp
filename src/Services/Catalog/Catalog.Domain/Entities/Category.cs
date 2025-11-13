
namespace Catalog.Domain.Entities;

public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Code { get; private set; } = string.Empty;
    public string? Description { get; private set; }

    private Category() { }
    private Category(string name,string code, string? description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Code = code;
        Description = description;
    }

    public static Category Create(string name, string code, string? description)
        => new Category(name, code, description);
}
