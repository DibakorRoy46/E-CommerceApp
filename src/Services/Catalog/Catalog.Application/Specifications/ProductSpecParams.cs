

using Catalog.Domain.Enums;

namespace Catalog.Application.Specifications;

public class ProductSpecParams
{
    public StatusEnum? Status { get; private set; }
    public bool? IsInStock { get; private set; }
    public int? BrandId { get; private set; }
    public decimal? MinPrice { get; private set; }
    public decimal? MaxPrice { get; private set; }
    public int? ProductHierarchyId { get; private set; }
    public SortByEnum? SortBy { get; private set; }
    public string? SearchTerm { get; private set; }

    private const int MaxPageSize = 50;

    public int PageIndex { get; private set; } = 1;

    private readonly int _pageSize = 10;
    public int PageSize
    {
        get => _pageSize;
        init => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    private ProductSpecParams() { }
}
