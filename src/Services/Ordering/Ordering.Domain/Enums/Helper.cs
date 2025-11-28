

namespace Ordering.Domain.Enums;

public enum OrderStatusEnum
{
    Pending = 0,
    Paid = 1,
    Deliveired = 2,
    Failed = 3,
    Cancelled = 4
}

public enum PaymentMethodEnum
{
    CreditCard = 1,
    DebitCard = 2,
    PayPal = 3,
    CashOnDelivery = 4
}