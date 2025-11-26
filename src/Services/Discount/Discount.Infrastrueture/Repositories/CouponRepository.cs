

using Dapper;
using Discount.Application.Interfaces;
using Discount.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Infrastrueture.Repositories;

public class CouponRepository : ICouponRepository
{
    private readonly IConfiguration _configuration;
    public CouponRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> DeleteCouponAsync(int id, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("ConnectionString:DefaultConnection"));

        // Open the connection with cancellation support
        await connection.OpenAsync(cancellationToken);

        var sql = "DELETE FROM Coupons WHERE Id = @Id";

        var affected = await connection.ExecuteAsync(sql, new { Id = id });

        return affected > 0;
    }

    public async Task<Coupon?> GetCouponByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("ConnectionString:DefaultConnection"));

        // Open the connection with cancellation support
        await connection.OpenAsync(cancellationToken);

        var sql = @"SELECT Id, Name, Code, Description, Amount, IsActive,CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
                    FROM Coupons WHERE Code = @code;";

        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(sql,new { code });
        return coupon;
    }


    public async Task<Coupon?> GetCouponByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("ConnectionString:DefaultConnection"));

        // Open the connection with cancellation support
        await connection.OpenAsync(cancellationToken);

        var sql = @"SELECT Id, Name, Code, Description, Amount, IsActive,CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
                    FROM Coupons WHERE Id = @Id;";

        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(sql, new { id });
        return coupon;
    }

    public async Task<List<Coupon>> GetCouponsAsync(int? isActive, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("ConnectionString:DefaultConnection"));

        // Open the connection with cancellation support
        await connection.OpenAsync(cancellationToken);

        var sql = @"SELECT Id, Name, Code, Description, Amount, IsActive,CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
                    FROM Coupons WHERE (@IsActive IS NULL OR IsActive = @IsActive);";

        var coupons = await connection.QueryAsync<Coupon>(sql, new { isActive });
        return coupons.ToList() ?? new List<Coupon>();
    }

    public async Task<Coupon> InsertCouponAsync(Coupon coupon, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("ConnectionString:DefaultConnection"));

        // Open the connection with cancellation support
        await connection.OpenAsync(cancellationToken);

        var sql = @"INSERT INTO Coupons ( Name, Code, Description, Amount, IsActive,CreatedBy, CreatedDate )
                    Values (@Name,@Code,@Description,@Amount,@IsActve,@CreatedBy,@CreatedDate);";

        await connection.ExecuteAsync(sql, new { coupon.Name, coupon.Code, coupon.Description, coupon.Amount, coupon.IsActive,
                                                    coupon.CreatedBy,coupon.CreatedDate});
        return coupon;
    }


    public async Task<Coupon> UpdateCouponAsync(Coupon coupon, CancellationToken cancellationToken = default)
    {
        await using var connection = new NpgsqlConnection(_configuration.GetValue<string>("ConnectionString:DefaultConnection"));

        // Open the connection with cancellation support
        await connection.OpenAsync(cancellationToken);

        var sql = @"UPDATE Coupons SET Name=@Name, Code=@Code, Description=@Description, Amount=@Amount, IsActive=@IsActive,
                    ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate WHERE Id=@Id ;";

        await connection.ExecuteAsync(sql, new
        {
            coupon.Name,coupon.Code,coupon.Description,coupon.Amount,coupon.IsActive,coupon.ModifiedBy,coupon.ModifiedDate,coupon.Id
        });
        return coupon;
    }
}
