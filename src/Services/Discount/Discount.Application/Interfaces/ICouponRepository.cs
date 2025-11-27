

using Discount.Domain.Entities;

namespace Discount.Application.Interfaces;

public interface ICouponRepository
{
    Task<List<Coupon>> GetCouponsAsync(int? isActive, CancellationToken cancellationToken = default);
    Task<Coupon?> GetCouponByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Coupon?> GetCouponByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<Coupon> InsertCouponAsync(Coupon coupon, CancellationToken cancellationToken = default);
    Task<Coupon> UpdateCouponAsync(Coupon coupon, CancellationToken cancellationToken = default);
    Task<bool> DeleteCouponAsync(int id, CancellationToken cancellationToken = default);
}
