

using Catalog.Domain.Enums;

namespace Catalog.Application.Specifications;

public class ProductSpecParams
{
    public StatusEnum? Status { get;  set; }
    public bool? IsInStock { get;  set; }
    public int? BrandId { get;  set; }
    public decimal? MinPrice { get;  set; }
    public decimal? MaxPrice { get;  set; }
    public int? ProductHierarchyId { get;  set; }
    public SortByEnum? SortBy { get;  set; }
    public string? SearchTerm { get;  set; }

    private const int MaxPageSize = 50;

    public int PageIndex { get;  set; } = 1;

    private readonly int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        init => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    public ProductSpecParams() { }
}
