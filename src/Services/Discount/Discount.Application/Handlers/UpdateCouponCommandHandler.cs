
using AutoMapper;
using Discount.Application.Commands;
using Discount.Application.DTOs;
using Discount.Application.Interfaces;
using Discount.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Discount.Application.Handlers;

public class UpdateCouponCommandHandler : IRequestHandler<UpdateCouponCommand, CouponDto>
{
    private readonly ICouponRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateCouponCommandHandler> _logger;

    public UpdateCouponCommandHandler(ICouponRepository repo, IMapper mapper, ILogger<UpdateCouponCommandHandler> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<CouponDto> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repo.GetCouponByIdAsync(request.Id);
        if(entity == null)
        {
            _logger.LogError("Coupon with Id {Id} not found.", request.Id);
            throw new KeyNotFoundException($"Coupon with Id {request.Id} not found.");
        }

        entity.Update(request.Name, request.Code, request.Description, request.Amount, request.IsActive, request.StartDate,
            request.EndDate, request.ModifiedBy);     
        await _repo.UpdateCouponAsync(entity);

        _logger.LogInformation("Coupon with Id {Id} code {code} updated successfully.", request.Id, request.Code);
        return _mapper.Map<CouponDto>(entity);
    }
}
