using AutoMapper;
using FarmFresh.Application.Dto.Request.Users;
using FarmFresh.Application.Enums;
using FarmFresh.Application.Interfaces.Services.Users;
using FarmFresh.Domain.Entities.Users;
using FarmFresh.Domain.RepoInterfaces.Users;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FarmFresh.Infrastructure.Service.Services.Users
{
    public class LocationService : ILocationService
    {
        #region Properties
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly string LocationiqAccessToken = "pk.e50a4fb4ec9a98ba147d9f53ed9052de";
        #endregion Properties

        #region Constructor
        public LocationService(
            ILocationRepository locationRepository,
            IMapper mapper,
            HttpClient httpClient
        )
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
            _httpClient = httpClient;
        }
        #endregion Constructor

        #region Private method



        private async Task SaveOrUpdateLocationAsync(LocationRequest locationRequest, int userId)
        {
            var locationByLatitudeAndLongitude = await GetAddressFromLocation(locationRequest.Latitude, locationRequest.Longitude);
            var location = new Location
            {
                UserId = userId,
                Latitude = locationRequest.Latitude,
                Longitude = locationRequest.Longitude,
                LocationType = locationRequest.LocationType,
                Address = locationRequest.Address,
                CreatedOn = DateTime.UtcNow
            };

            UpdateLocationFromAddress(location, locationByLatitudeAndLongitude);

            await SaveOrUpdateLocationAsync(location);
        }

        private async Task SaveOrUpdateLocationAsync(Location location, LocationRequest locationRequest)
        {
            var locationByLatitudeAndLongitude = await GetAddressFromLocation(locationRequest.Latitude, locationRequest.Longitude);
            location.Latitude = locationRequest.Latitude;
            location.Longitude = locationRequest.Longitude;
            location.LocationType = locationRequest.LocationType;
            location.Address = locationRequest.Address;
            location.UpdatedOn = DateTime.UtcNow;

            UpdateLocationFromAddress(location, locationByLatitudeAndLongitude);

            await SaveOrUpdateLocationAsync(location);
        }

        private async Task SaveOrUpdateLocationAsync(Location location)
        {
            await _locationRepository.UpdateAsync(location);
            await _locationRepository.SaveChangesAsync();
        }

        private async Task<LocationResponse> GetAddressFromLocation(decimal latitude, decimal longitude)
        {
            string apiUrl = $"v1/reverse?key={LocationiqAccessToken}&lat={latitude}&lon={longitude}&format=json";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                return LocationResponse.ParseAddressFromResponse(responseContent);
            }

            return null;
        }

        private static void UpdateLocationFromAddress(Location location, LocationResponse locationResponse)
        {
            if (locationResponse is not null)
            {
                location.Address = string.IsNullOrWhiteSpace(location.Address) ? locationResponse.DisplayName : location.Address;
                location.City = locationResponse.Address.City;
                location.State = locationResponse.Address.State;
                location.ZipCode = locationResponse.Address.PostalCode;
                location.Country = locationResponse.Address.Country;
            }
        }


        #endregion Private method

        #region Get

        public async Task<Application.Dto.Response.Users.LocationResponse> GetAsync(int userId, LocationType locationType = LocationType.Home)
        {
            var location = await _locationRepository.GetByCondition(x => x.UserId == userId && x.LocationType == locationType).FirstOrDefaultAsync();
            return _mapper.Map<Application.Dto.Response.Users.LocationResponse>(location);
        }

        #endregion Get

        #region Update
        public async Task UpsertAsync(LocationRequest locationRequest, int userId)
        {
            var location = await _locationRepository.GetByCondition(x => x.UserId == userId && x.LocationType == locationRequest.LocationType).FirstOrDefaultAsync();

            if (location is null)
            {
                await SaveOrUpdateLocationAsync(locationRequest, userId);
            }
            else
            {
                await SaveOrUpdateLocationAsync(location, locationRequest);
            }
        }
        
        #endregion Update
    }

    #region Remote response class

    public class AddressDetails
    {
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
    }

    public class LocationResponse
    {
        public AddressDetails Address { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        public static LocationResponse ParseAddressFromResponse(string jsonResponse)
        {
            return JsonConvert.DeserializeObject<LocationResponse>(jsonResponse);
        }
    }

    #endregion Remote response class
}
