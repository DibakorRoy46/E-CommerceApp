
using Discount.Application.Commands;
using Discount.Application.Queries;
using Discount.Grpc.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Discount.API.Services;

public class DiscountService  :DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IMediator _mediator;

    public DiscountService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<CouponModel> GetDiscountById(GetDiscountByIdRequest request, ServerCallContext context)
    {
        var query = new GetCouponByIdQuery(request.Id);
        var result=await _mediator.Send(query);
        return new CouponModel()
        {
            Id = result.Id,
            Name = result.Name,
            Code = result.Code,
            Description = result.Description,
            IsActive = result.IsActive,
            Amount = result.Amount.ToString()
        };
    }

    public async override Task<CouponModel> GetDiscountByCode(GetDiscountByCodeRequest request, ServerCallContext context)
    {
        var query = new GetCouponByCodeQuery(request.Code);
        var result = await _mediator.Send(query);
        return new CouponModel()
        {
            Id = result.Id,
            Name = result.Name,
            Code = result.Code,
            Description = result.Description,
            IsActive = result.IsActive,
            Amount = result.Amount.ToString()
        };
    }

    public async override Task<GetDiscountsResponse> GetDiscounts(GetDiscountsRequest request, ServerCallContext context)
    {
        var query = new GetCouponsQuery(request.IsActive);
        var result = await _mediator.Send(query);

        var response = new GetDiscountsResponse();

        response.Coupons.AddRange(
            result.Select(x => new CouponModel
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Description = x.Description,
                IsActive = x.IsActive,
                Amount = x.Amount.ToString()
            })
        );

        return response;
    }

    public async override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var command= new CreateCouponCommand(request.Coupon.Name,request.Coupon.Code,request.Coupon.Description,
                    Convert.ToDecimal(request.Coupon.Amount), request.Coupon.IsActive,string.Empty);

        var result= await _mediator.Send(command);

        return new CouponModel()
        {
            Id = result.Id,
            Name = result.Name,
            Code = result.Code,
            Description = result.Description,
            IsActive = result.IsActive,
            Amount = result.Amount.ToString()
        };
    }

    public async override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var command = new UpdateCouponCommand(request.Coupon.Id, request.Coupon.Name, request.Coupon.Code, request.Coupon.Description,
                    Convert.ToDecimal(request.Coupon.Amount), request.Coupon.IsActive, string.Empty);

        var result = await _mediator.Send(command);

        return new CouponModel()
        {
            Id = result.Id,
            Name = result.Name,
            Code = result.Code,
            Description = result.Description,
            IsActive = result.IsActive,
            Amount = result.Amount.ToString()
        };
    }

    public async override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var command = new DeleteCouponCommand(request.Id);
        var result= await _mediator.Send(command);
        return new DeleteDiscountResponse() { Success=result };
    }
}
