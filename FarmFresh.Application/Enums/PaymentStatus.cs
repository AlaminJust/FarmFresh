using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [Description("Shipped")]
        Shipped = 3,
        [Description("Delivered")]
        Delivered = 4,
        [Description("Cancelled")]
        Cancelled = 5
    }
}
