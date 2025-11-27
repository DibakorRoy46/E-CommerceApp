
using Discount.Grpc.Protos;

namespace Basket.Application.GrpcService;

public class DiscountGrpcService
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _grpcClient;

    public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient grpcClient)
    {
        _grpcClient = grpcClient;
    }

    public async Task<CouponModel> GetCouponByCode(string code)
    {
        var discountRequest= new GetDiscountByCodeRequest { Code = code };
        return await _grpcClient.GetDiscountByCodeAsync(discountRequest);
    }
}
