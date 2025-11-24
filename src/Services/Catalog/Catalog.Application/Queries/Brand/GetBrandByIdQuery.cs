

using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Queries;

public class GetBrandByIdQuery :IRequest<BrandDto>
{
    public int Id { get; }

    public GetBrandByIdQuery(int id)
    {
        Id = id;
    }
}
