using FarmFresh.Application.Enums;

namespace FarmFresh.Application.Dto.Request.Users
{
    public class LocationRequest
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public LocationType LocationType { get; set; }
    }
}
