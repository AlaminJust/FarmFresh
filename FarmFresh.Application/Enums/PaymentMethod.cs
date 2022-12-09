using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Application.Enums
{
    public enum PaymentMethod: byte
    {
        [Description("Cash on delivery")]
        CashOnDelivery = 1,
        [Description("Bkash card")]
        Bkash = 2
    }
}
