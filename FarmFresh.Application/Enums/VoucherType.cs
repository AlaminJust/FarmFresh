using System.ComponentModel;

namespace FarmFresh.Application.Enums
{
    public enum VoucherType: byte
    {
        [Description("Percentage")]
        Percentage = 1,
        
        [Description("Fixed Amount")]
        Fixed = 2
    }
}
