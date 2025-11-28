

namespace Ordering.Application.DTOs;

public record OrderItemDto(int OrderId, string ProductId , string ProductName ,string ProductCode ,decimal UnitPrice ,
                            int Quantity ,decimal ItemWiseDicount);