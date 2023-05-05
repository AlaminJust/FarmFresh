using System.ComponentModel;

namespace FarmFresh.Application.Enums
{
    public enum LocationType: byte
    {
        [Description("Home")]
        Home = 1,
        [Description("Work")]
        Work = 2,
        [Description("Other")]
        Other = 3
    }
}
