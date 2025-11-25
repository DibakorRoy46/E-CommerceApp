

using AutoMapper;
using Discount.Application.DTOs;
using Discount.Application.Interfaces;
using Discount.Application.Quiries;
using MediatR;

namespace Discount.Application.Handlers;

public class GetCouponByIdQueryHandler : IRequestHandler<GetCouponByIdQuery, CouponDto>
{
    private readonly ICouponRepository _repo;
    private readonly IMapper _mapper;

    public GetCouponByIdQueryHandler(ICouponRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<CouponDto> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
    {
        var entity=await _repo.GetCouponByIdAsync(request.Id,cancellationToken);
        if (entity == null)
        {
            throw new KeyNotFoundException($"No Coupon found with Id {request.Id}");
        }

        return _mapper.Map<CouponDto>(entity);
    }
}
