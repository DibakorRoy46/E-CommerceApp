

namespace Discount.Application.DTOs;

public record CouponDto
(
    int Id,
    string Name,
    string Code,
    string Description,
    decimal Amount,
    int IsActive,
    string CreatedBy,
    DateTimeOffset CreatedDate,
    string ModifiedBy,
    DateTimeOffset ModifiedDate
);
