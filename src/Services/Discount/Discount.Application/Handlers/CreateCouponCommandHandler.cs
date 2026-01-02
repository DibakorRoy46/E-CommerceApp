
using AutoMapper;
using Discount.Application.Commands;
using Discount.Application.DTOs;
using Discount.Application.Interfaces;
using Discount.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Discount.Application.Handlers;

public class CreateCouponCommandHandler : IRequestHandler<CreateCouponCommand, CouponDto>
{
    private readonly ICouponRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCouponCommand> _logger;

    public CreateCouponCommandHandler(ICouponRepository repo, IMapper mapper, ILogger<CreateCouponCommand> logger)
    {
        _repo = repo;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<CouponDto> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
    {
        var entity= new Coupon(request.Name, request.Code, request.Description, request.Amount, request.IsActive,
            request.StartDate,request.EndDate,request.CreatedBy);
        await _repo.InsertCouponAsync(entity);
        _logger.LogInformation("Coupon with Id {CouponId} with code {code} created successfully.", entity.Id, entity.Code);
        return _mapper.Map<CouponDto>(entity);
    }
}
