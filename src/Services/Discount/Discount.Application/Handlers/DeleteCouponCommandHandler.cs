

using AutoMapper;
using Discount.Application.Commands;
using Discount.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Discount.Application.Handlers;

public class DeleteCouponCommandHandler : IRequestHandler<DeleteCouponCommand, bool>
{
    private readonly ICouponRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteCouponCommandHandler> _logger;

    public DeleteCouponCommandHandler(ICouponRepository repo, IMapper mapper, ILogger<DeleteCouponCommandHandler> logger)
    {
        _mapper = mapper;
        _repo = repo;
        _logger = logger;
    }
    public async Task<bool> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
    {
        var entity=await _repo.GetCouponByIdAsync(request.Id);
        if (entity == null)
        {
            throw new Exception($"No Coupon found for delete with id {request.Id}");
        }

        await _repo.DeleteCouponAsync(request.Id);
        _logger.LogInformation("Coupon with Id {CouponId} with code {code} deleted successfully.", request.Id, entity.Code);
        return true;
    }
}
