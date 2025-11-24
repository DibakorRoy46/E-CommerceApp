

using Catalog.Application.DTOs;
using Catalog.Domain.Enums;
using MediatR;

namespace Catalog.Application.Queries;

public class GetBrandsQuery : IRequest<List<BrandDto>>
{
    public StatusEnum Status { get;}

    public GetBrandsQuery(StatusEnum status)
    {
        Status = status;
    }
}