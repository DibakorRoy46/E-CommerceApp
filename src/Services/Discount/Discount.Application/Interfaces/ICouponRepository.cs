

using Discount.Domain.Entities;

namespace Discount.Application.Interfaces;

public interface ICouponRepository
{
    Task<List<Coupon>> GetCouponsAsync(int? isActive, CancellationToken cancellationToken = default);
    Task<Coupon> GetCouponByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Coupon> GetCouponByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<Coupon> InsertCouponAsync(Coupon coupon);
    Task<Coupon> UpdateCouponAsync(Coupon coupon);
    Task<bool> DeleteCouponAsync(int id);
    Task SaveChangesAsync(CancellationToken cancellationToken=default);
}
