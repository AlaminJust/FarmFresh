﻿using FarmFresh.Application.Enums;

namespace FarmFresh.Application.Dto.Request.Users
{
    public class LocationRequest
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Address { get; set; }
        public LocationType LocationType { get; set; }
    }

    public class LocationQueueRequest : LocationRequest
    {
        public int UserId { get; set; } 
    }
}
