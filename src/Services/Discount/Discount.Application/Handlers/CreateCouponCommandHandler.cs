
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
        var entity= new Coupon(request.Name, request.Code, request.Description, request.Amount, request.IsActive,request.CreatedBy);
        await _repo.InsertCouponAsync(entity);
        return _mapper.Map<CouponDto>(entity);
    }
}
