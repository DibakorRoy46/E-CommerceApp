
using Ordering.Domain.Enums;

namespace Ordering.Application.Specs;

public class OrderSpec
{
    public OrderStatusEnum? OrderStatus { get; private set; }
    public PaymentMethodEnum? PaymentMethod { get; private set; }

    public OrderSpec(OrderStatusEnum? orderStatus, PaymentMethodEnum paymentMethod)
    {
        OrderStatus = orderStatus;
        PaymentMethod = paymentMethod;
    }
}
