using FarmFresh.Application.Dto.Request.Users;
using FarmFresh.Application.Dto.Response.Users;
using FarmFresh.Application.Interfaces.Services.Users;
using FarmFresh.Domain.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FarmFresh.Controllers.Users
{
    [Route("api/user-management")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(
                IUserService userService,
                IConfiguration configuration
            )
        {
            _userService = userService;
            _configuration = configuration;
        }

        #region Properties
        private Task<Claim[]> GetValidClaims(LoginResponse loginResponse)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration.GetSection("Jwt").GetSection("Subject").Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.Name, loginResponse.UserName),
                new Claim("UserId", loginResponse.Id.ToString()),
                new Claim(ClaimTypes.Email, loginResponse.Email),
                new Claim(ClaimTypes.Role, "User")
            };

            return Task.FromResult(claims.ToArray());
        }
        #endregion Properties

        #region Get
        [HttpGet("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginAsync([FromQuery] LoginRequest loginRequest)
        {
            var loginResponse = await _userService.LoginAsync(loginRequest);
            if (loginResponse is not null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt").GetSection("SecretKey").Value));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity
                    (
                        await GetValidClaims(loginResponse)
                    ),
                    Expires = DateTime.UtcNow.AddHours(12),
                    SigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                loginResponse.Token = tokenHandler.WriteToken(token);
                return Ok(loginResponse);
            }
            else
            {
                return BadRequest("Invalid username or password");
            }
        }
        #endregion Get

        #region Save
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromBody] UserRequest userRequest)
        {
            await _userService.AddAsync(userRequest);
            return Ok();
        }
        #endregion Save
    }
}
