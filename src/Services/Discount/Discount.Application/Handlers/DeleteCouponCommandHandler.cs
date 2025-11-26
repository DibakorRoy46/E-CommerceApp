

using AutoMapper;
using Discount.Application.Commands;
using Discount.Application.Interfaces;
using MediatR;

namespace Discount.Application.Handlers;

public class DeleteCouponCommandHandler : IRequestHandler<DeleteCouponCommand, bool>
{
    private readonly ICouponRepository _repo;
    private readonly IMapper _mapper;

    public DeleteCouponCommandHandler(ICouponRepository repo, IMapper mapper)
    {
        _mapper = mapper;
        _repo = repo;
    }
    public async Task<bool> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
    {
        var entity=await _repo.GetCouponByIdAsync(request.Id);
        if (entity == null)
        {
            throw new Exception($"No Coupon found for delete with id {request.Id}");
        }

        await _repo.DeleteCouponAsync(request.Id);
        return true;
    }
}
