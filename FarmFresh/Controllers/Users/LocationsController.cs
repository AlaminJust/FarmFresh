using FarmFresh.Application.Dto.Request.Users;
using FarmFresh.Application.Dto.Response.Users;
using FarmFresh.Application.Enums;
using FarmFresh.Application.Helpers;
using FarmFresh.Application.Interfaces.Handlers;
using FarmFresh.Application.Interfaces.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Api.Controllers.Users
{
    [Route("api/location-management")]
    [ApiController]
    public class LocationsController : ApiControllerBase
    {
        #region Properties
        private readonly ILocationService _locationService;
        private readonly LocationQueueHelper _locationQueueHelper;
        #endregion Properties

        #region Ctor
        public LocationsController(
                ILocationService locationService,
                LocationQueueHelper locationQueueHelper
            )
        {
            _locationService = locationService;
            _locationQueueHelper = locationQueueHelper;
        }
        #endregion Ctor

        #region Get
        [HttpGet("location/{type=1}")]
        [Authorize]
        [ProducesResponseType(typeof(LocationResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromRoute] LocationType type)
        {
            var location = await _locationService.GetAsync(UserId, type);
            return Ok(location);
        }

        [HttpGet("location")]
        [Authorize]
        [ProducesResponseType(typeof(List<LocationResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllLocationByUserIdAsync()
        {
            var locations = await _locationService.GetAllLocationByUserIdAsync(UserId);
            return Ok(locations);
        }

        #endregion Get

        #region Save
        [HttpPost("location")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult SaveAsync([FromBody] LocationRequest locationRequest)
        {
            var location = new LocationQueueRequest
            {
                UserId = UserId,
                Latitude = locationRequest.Latitude,
                Longitude = locationRequest.Longitude,
                LocationType = locationRequest.LocationType,
                Address = locationRequest.Address
            };

            _ = _locationQueueHelper.Enqueue(location);
            
            return Ok();
        }
        #endregion Save
    }
}
