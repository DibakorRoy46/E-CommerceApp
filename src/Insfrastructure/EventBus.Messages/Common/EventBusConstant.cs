
namespace EventBus.Messages.Common;

public class EventBusConstant
{
    public const string BasketCheckoutQueue = "basketcheckout-queue";
    public const string OrderCreatedQueue = "ordercreated-queue";
    public const string PaymentSuccessQueue = "paymentsuccess-queue";
    public const string PaymentFailedQueue = "paymentfailed-queue";
    public const string PaymentCompletedMessageQueue = "paymentcompletedmessage-queue";
    public const string OrderCreatedMessageQueue = "ordercreatedmessage-queue";
}
