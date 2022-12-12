using System.ComponentModel;

namespace FarmFresh.Application.Enums
{
    public enum DiscountType : byte
    {
        [Description("Percentage")]
        Percentage = 1,
        [Description("Fixed")]
        Fixed = 2
    }
}
