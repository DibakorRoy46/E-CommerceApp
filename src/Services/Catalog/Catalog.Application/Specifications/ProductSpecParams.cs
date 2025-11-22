

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

    public ProductSpecParams(StatusEnum? status, bool? isInStock, int? brandId, decimal? minPrice, decimal? maxPrice, int? productHierarchyId,
                        SortByEnum? sortBy, string? searchTerm, int pageIndex = 1, int pageSize = 10)
    {
        if (minPrice.HasValue && maxPrice.HasValue && minPrice.Value > maxPrice.Value)
        {
            throw new ArgumentException("MinPrice cannot be greater than MaxPrice.", nameof(minPrice));
        }

        Status = status;
        IsInStock = isInStock;
        BrandId = brandId;
        MinPrice = minPrice;
        MaxPrice = maxPrice;
        ProductHierarchyId = productHierarchyId;
        SortBy = sortBy;
        SearchTerm = searchTerm;

        PageIndex = pageIndex;
        PageSize = pageSize;
    }

    public ProductSpecParams(StatusEnum? status, bool? isInStock, int? brandId, decimal? minPrice, decimal? maxPrice, int? productHierarchyId,
        SortByEnum? sortBy, string? searchTerm) : this(status, isInStock, brandId, minPrice, maxPrice, productHierarchyId, sortBy, searchTerm, 1, 10)
    {
        // Passes to the main constructor with default pagination values
    }
}
