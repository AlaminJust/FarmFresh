using System.ComponentModel;

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
