using AutoMapper;
using FarmFresh.Application.Dto.Request.Users;
using FarmFresh.Application.Dto.Response.Users;
using FarmFresh.Application.Interfaces.Services.Users;
using FarmFresh.Domain.Entities.Users;
using FarmFresh.Domain.RepoInterfaces.Users;
using Microsoft.EntityFrameworkCore;

namespace FarmFresh.Infrastructure.Service.Services.Users
{
    public class LocationService : ILocationService
    {
        #region Properties
        private ILocationRepository _locationRepository;
        private IMapper _mapper;
        private readonly IMapper mapper;
        #endregion Properties

        #region Ctor
        public LocationService(
                ILocationRepository locationRepository,
                IMapper mapper
            )
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        #endregion Ctor

        #region Get
        public async Task<LocationResponse> GetAsync(int userId)
        {
            var location = await _locationRepository.GetByCondition(x => x.UserId == userId).FirstOrDefaultAsync();
            
            if(location == null)
            {
                return null;
            }
            
            return _mapper.Map<LocationResponse>(location);
        }

        #endregion Get


        #region Save
        public async Task UpsertAsync(LocationRequest locationRequest, int userId)
        {
            var location = await _locationRepository.GetByCondition(x => x.UserId == userId && x.LocationType == locationRequest.LocationType).FirstOrDefaultAsync();

            if (location is null)
            {
                await SaveAsync(locationRequest, userId);
            }
            else
            {
                await UpdateAsync(location, locationRequest);
            }
        }


        private async Task SaveAsync(LocationRequest locationRequest, int userId)
        {
            var location = new Location
            {
                UserId = userId,
                Latitude = locationRequest.Latitude,
                Longitude = locationRequest.Longitude,
                LocationType = locationRequest.LocationType,
                CreatedOn = DateTime.UtcNow,
            };

            await _locationRepository.UpdateAsync(location);
            await _locationRepository.SaveChangesAsync();
        }

        #endregion Save

        #region Update 
        private async Task UpdateAsync(Location location, LocationRequest locationRequest)
        {
            location.Latitude = locationRequest.Latitude;
            location.Longitude = locationRequest.Longitude;
            location.LocationType = locationRequest.LocationType;
            location.UpdatedOn = DateTime.UtcNow;
            
            await _locationRepository.UpdateAsync(location);
            await _locationRepository.SaveChangesAsync();
        }
        #endregion Update
    }
}
