
using Catalog.Application.DTOs;
using Catalog.Domain.Enums;
using MediatR;

namespace Catalog.Application.Queries;

public class GetProductsQuery: IRequest<List<ProductDto>>
{
    public StatusEnum? Status { get; private set; }

    public GetProductsQuery(StatusEnum? status)
    {
        Status = status;
    }
}
