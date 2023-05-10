using System.ComponentModel;

namespace FarmFresh.Application.Enums
{
    public enum PaymentStatus: byte
    {
        [Description("Pending")]
        Pending = 1,
        [Description("Success")]
        Success = 2,
        [Description("Failed")]
        Failed = 3,
        [Description("Refunded")]
        Refunded = 4
    }

    public enum OrderStatus : byte
    {
        [Description("Pending")]
        Pending = 1,
        [Description("Processing")]
        Processing = 2,
        [Description("Verified")]
        Verified = 3,
        [Description("Shipped")]
        Shipped = 4,
        [Description("Delivered")]
        Delivered = 5,
        [Description("Cancelled")]
        Cancelled = 6,
        [Description("Refunded")]
        Refunded = 7,
        [Description("Returned")]
        Returned = 8,
        [Description("Failed")]
        Failed = 9,
        [Description("Completed")]
        Completed = 10,
        [Description("On Hold")]
        OnHold = 11,
        [Description("Awaiting Payment")]
        AwaitingPayment = 12,
    }
}
