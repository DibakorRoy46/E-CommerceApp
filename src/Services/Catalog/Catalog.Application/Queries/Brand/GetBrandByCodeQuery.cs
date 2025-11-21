
using Catalog.Application.DTOs;
using MediatR;

namespace Catalog.Application.Queries;

public class GetBrandByCodeQuery : IRequest<BrandDto>
{
    public string Code { get; }

    public GetBrandByCodeQuery(string code)
    {
        Code = code;
    }
}
