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
}
