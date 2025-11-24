
using Catalog.Domain.Enums;

namespace Catalog.Application.Requests;

public class ProductRequest
{
    public StatusEnum? Status { get; set; }
}
