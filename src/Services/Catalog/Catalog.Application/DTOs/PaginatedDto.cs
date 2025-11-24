
namespace Catalog.Application.DTOs;

public class PaginatedDto<T> 
{
    public IEnumerable<T> Datas { get; private set; }
    public int PageIndex { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    public PaginatedDto(int pageIndex, int pageSize, int totalCount, IEnumerable<T> datas)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = totalCount;
        Datas = datas;
    }
}
