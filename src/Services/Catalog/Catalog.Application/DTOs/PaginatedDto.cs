
namespace Catalog.Application.DTOs;

public class PaginatedDto<T> 
{
    public required IEnumerable<T> Datas { get; init; }
    public required int PageIndex { get; init; }
    public required int PageSize { get; init; }
    public required int TotalCount { get; init; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
