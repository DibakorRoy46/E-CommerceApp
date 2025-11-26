
using AutoMapper;
using Discount.Application.Commands;
using Discount.Application.DTOs;
using Discount.Application.Interfaces;
using Discount.Domain.Entities;
using MediatR;

namespace Discount.Application.Handlers;

public class UpdateCouponCommandHandler : IRequestHandler<UpdateCouponCommand, CouponDto>
{
    private readonly ICouponRepository _repo;
    private readonly IMapper _mapper;

    public UpdateCouponCommandHandler(ICouponRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    public async Task<CouponDto> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetCouponByIdAsync(request.Id);
        entity.Update(entity.Name, entity.Code, entity.Description, entity.Amount, entity.IsActive, entity.ModifiedBy);
        
        await _repo.UpdateCouponAsync(entity);
        return _mapper.Map<CouponDto>(entity);
    }
}
