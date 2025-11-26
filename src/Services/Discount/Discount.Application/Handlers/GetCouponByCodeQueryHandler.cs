
using AutoMapper;
using Discount.Application.DTOs;
using Discount.Application.Interfaces;
using Discount.Application.Queries;
using MediatR;

namespace Discount.Application.Handlers;

public class GetCouponByCodeQueryHandler : IRequestHandler<GetCouponByCodeQuery, CouponDto>
{
    private readonly ICouponRepository _repo;
    private readonly IMapper _mapper;

    public GetCouponByCodeQueryHandler(ICouponRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<CouponDto> Handle(GetCouponByCodeQuery request, CancellationToken cancellationToken)
    {
        var coupon= await _repo.GetCouponByCodeAsync(request.Code, cancellationToken);

        if (coupon == null)
        {
            throw new KeyNotFoundException($"Coupon is not found name {request.Code}");
        }
        return _mapper.Map<CouponDto>(coupon); 
    }
}
