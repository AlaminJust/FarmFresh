using FarmFresh.Application.Enums;

namespace FarmFresh.Application.Dto.Response.Users
{
    public class LocationResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public float? Speed { get; set; }
        public double? Altitude { get; set; }
        public float? Accuracy { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public LocationType LocationType { get; set; }
    }
}