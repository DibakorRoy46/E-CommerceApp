
using AutoMapper;
using Discount.Application.DTOs;
using Discount.Application.Interfaces;
using Discount.Application.Queries;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Discount.Application.Handlers;

public class GetCouponByCodeQueryHandler : IRequestHandler<GetCouponByCodeQuery, CouponDto>
{
    private readonly ICouponRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<GetCouponByCodeQueryHandler> _logger;

    public GetCouponByCodeQueryHandler(ICouponRepository repo, IMapper mapper, ILogger<GetCouponByCodeQueryHandler> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<CouponDto> Handle(GetCouponByCodeQuery request, CancellationToken cancellationToken)
    {
        var coupon= await _repo.GetCouponByCodeAsync(request.Code, cancellationToken);

        if(coupon == null)
        {
            _logger.LogWarning("Coupon with code '{Code}' not found.", request.Code);
            return CouponDto.Empty;
        }
        if(coupon.StartDate.Date <= DateTime.UtcNow.Date && coupon.EndDate.Date >= DateTime.UtcNow.Date && coupon.IsActive == 1)
        {
            _logger.LogInformation("Coupon with code '{Code}' is valid.", request.Code);
            return _mapper.Map<CouponDto>(coupon);
        }
        else
        {
            _logger.LogWarning("Coupon with code '{Code}' is not valid.", request.Code);
            return CouponDto.Empty;
        }      
    }
}
