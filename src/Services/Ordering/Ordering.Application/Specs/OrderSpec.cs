
using Ordering.Domain.Enums;

namespace Ordering.Application.Specs;

public class OrderSpec
{
    public OrderStatusEnum? OrderStatus { get; private set; }
    public PaymentMethodEnum? PaymentMethod { get; private set; }
    public DateTime? StartDate { get; private set; }

    public DateTime? EndDate { get; private set; }

    private OrderSpec() { }

    //public OrderSpec(OrderStatusEnum? orderStatus, PaymentMethodEnum paymentMethod, DateTime? startDate, DateTime? endDate)
    //{
    //    OrderStatus = orderStatus;
    //    PaymentMethod = paymentMethod;
    //    StartDate = startDate;
    //    EndDate = endDate;
    //}
}
