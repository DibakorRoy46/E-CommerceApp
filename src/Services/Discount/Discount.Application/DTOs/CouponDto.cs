

namespace Discount.Application.DTOs;

public record CouponDto
(
    int Id,
    string Name,
    string Code,
    string Description,
    decimal Amount,
    int IsActive,
    DateTime StartDate,
    DateTime EndDate,
    string CreatedBy,
    DateTimeOffset CreatedDate,
    string ModifiedBy,
    DateTimeOffset ModifiedDate
)
{
    public static CouponDto Empty => new(
        0,
        string.Empty,
        string.Empty,
        string.Empty,
        0m,
        0,
        DateTime.MinValue,
        DateTime.MinValue,
        string.Empty,
        DateTimeOffset.MinValue,
        string.Empty,
        DateTimeOffset.MinValue
    );
};
