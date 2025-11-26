
using AutoMapper;
using Discount.Application.DTOs;
using Discount.Application.Interfaces;
using Discount.Application.Queries;
using MediatR;

namespace Discount.Application.Handlers;

public class GetCouponsQueryHandler : IRequestHandler<GetCouponsQuery, List<CouponDto>>
{
    private readonly ICouponRepository _repo;
    private readonly IMapper _mapper;

    public GetCouponsQueryHandler(ICouponRepository repo, IMapper mapper)
    {
        _mapper = mapper;   
        _repo = repo;
    }
    public async Task<List<CouponDto>> Handle(GetCouponsQuery request, CancellationToken cancellationToken)
    {
        var result=await _repo.GetCouponsAsync(request.IsActive, cancellationToken);

        if (request is null)
            return new List<CouponDto>();

        return _mapper.Map<List<CouponDto>>(result) ;
    }
}
