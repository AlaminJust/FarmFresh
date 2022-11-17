using FarmFresh.Application.Dto.Request.Users;
using FarmFresh.Application.Dto.Response.Users;
using FarmFresh.Application.Interfaces.Services.Users;
using FarmFresh.Domain.Dto.Request.Users;
using Microsoft.AspNetCore.Mvc;
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
        #region Properties
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IRoleService _roleService;
        private readonly ILogger<UserController> _logger;
        #endregion Properties

        #region Constructor
        public UserController(
                IUserService userService,
                IConfiguration configuration,
                IRoleService roleService,
                ILogger<UserController> logger
            )
        {
            _userService = userService;
            _configuration = configuration;
            _roleService = roleService;
            _logger = logger;
        }
        #endregion Constructor

        #region Private method
        private async Task<Claim[]> GetValidClaims(LoginResponse loginResponse)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration.GetSection("Jwt").GetSection("Subject").Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.Name, loginResponse.UserName),
                new Claim("UserId", loginResponse.Id.ToString()),
                new Claim(ClaimTypes.Email, loginResponse.Email),
            };
            
            var roles = await _roleService.GetRoleNamesByUserIdAsync(loginResponse.Id);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            
            return claims.ToArray();
        }
        #endregion Private method

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
                _logger.LogInformation("Invalid login attempt for user: {0}", loginRequest.UserName);
                return BadRequest("Invalid username or password");
            }
        }
        #endregion Get

        #region Save
        [HttpPost("signup")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromBody] UserRequest userRequest)
        {
            await _userService.AddAsync(userRequest);
            return Ok();
        }
        #endregion Save
    }
}
