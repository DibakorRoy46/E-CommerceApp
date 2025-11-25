
using AutoMapper;
using Discount.Application.Commands;
using Discount.Application.DTOs;
using Discount.Application.Interfaces;
using Discount.Domain.Entities;
using MediatR;

namespace Discount.Application.Handlers;

public class CreateCouponCommandHandler : IRequestHandler<CreateCouponCommand, CouponDto>
{
    private readonly ICouponRepository _repo;
    private readonly IMapper _mapper;

    public CreateCouponCommandHandler(ICouponRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<CouponDto> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
    {
        var entity= _mapper.Map<Coupon>(request);
        await _repo.InsertCouponAsync(entity);
        await _repo.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CouponDto>(entity);
    }
}
