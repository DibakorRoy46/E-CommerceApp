
namespace Catalog.Domain.Enums;

public enum StatusEnum
{
    NoFilter=0,
    Initiate=1,
    Authenticated=2,
    Authroized=3,
    Rejected = 4
}

public enum ProductHierarchyLevelEnum
{
    Division=1,
    Category = 2,
    SubCategory = 3,
}

public enum ProductUnitEnum
{
    Pcs=1,
    Kg=2,
    Gram=3,
    Litre=4,
    Ml=5
}

public enum SortByEnum
{
    NameAsc=1,
    NameDesc=2,
    PriceAsc=3,
    PriceDesc=4,
    CreatedDateAsc=5,
    CreatedDateDesc=6
}