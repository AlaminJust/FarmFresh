using FarmFresh.Application.Dto.Request.Users;
using FarmFresh.Application.Interfaces.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Api.Controllers.Users
{
    [Route("api/location-management")]
    [ApiController]
    public class LocationsController : ApiControllerBase
    {
        #region Properties
        private readonly ILocationService _locationService;
        #endregion Properties

        #region Ctor
        public LocationsController(
                ILocationService locationService
            )
        {
            _locationService = locationService;
        }
        #endregion Ctor

        #region Save
        [HttpPost("save")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SaveAsync([FromBody] LocationRequest locationRequest)
        {
            await _locationService.UpsertAsync(locationRequest, UserId);
            return Ok();
        }
        #endregion Save
    }
}
