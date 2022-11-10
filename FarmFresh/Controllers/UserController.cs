using FarmFresh.Application.Dto.Request.Users;
using FarmFresh.Application.Interfaces.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FarmFresh.Controllers
{
    [Route("api/user-management")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(
                IUserService userService
            )
        {
            _userService = userService;
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromBody] UserRequest userRequest)
        {
            await _userService.AddAsync(userRequest);
            return Ok();
        }
    }
}
