using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using FarmFresh.Application.Dto.Request.Users;
using FarmFresh.Application.Dto.Response.Users;
using FarmFresh.Application.Interfaces.Services.Users;
using FarmFresh.Domain.Dto.Request.Users;
using FarmFresh.Api.Controllers;

namespace FarmFresh.Controllers.Users
{
    [Route("api/user-management")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        #region Properties
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IRoleService _roleService;
        private readonly ILogger<UserController> _logger;
        private readonly IRefreshTokenService _refreshTokenService;

        private const int TokenExpiryInMinutes = 60 * 24;
        #endregion Properties

        #region Ctor
        public UserController(
            IUserService userService,
            IConfiguration configuration,
            IRoleService roleService,
            ILogger<UserController> logger,
            IRefreshTokenService refreshTokenService)
        {
            _userService = userService;
            _configuration = configuration;
            _roleService = roleService;
            _logger = logger;
            _refreshTokenService = refreshTokenService;
        }
        #endregion Ctor

        #region Private Method
        private Task<string> GenerateToken(IEnumerable<Claim> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt").GetSection("SecretKey").Value));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(TokenExpiryInMinutes),
                SigningCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Task.FromResult(tokenHandler.WriteToken(token));
        }

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

        #endregion Private Method

        #region Signup

        [HttpPost("signup")]
        public async Task<IActionResult> CreateAsync([FromBody] UserRequest userRequest)
        {
            await _userService.AddAsync(userRequest);
            return Ok();
        }

        #endregion Signup

        #region Login
        [HttpGet("login")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginAsync([FromQuery] LoginRequest loginRequest)
        {
            var loginResponse = await _userService.LoginAsync(loginRequest);
            if (loginResponse == null)
            {
                _logger.LogInformation("Invalid login attempt for user: {0}", loginRequest.UserName);
                return BadRequest("Invalid username or password");
            }

            var refreshToken = await _refreshTokenService.AddOrUpdateAsync(loginResponse.Id);

            var claims = await GetValidClaims(loginResponse);
            loginResponse.Token = await GenerateToken(claims); ;
            loginResponse.RefreshToken = refreshToken;

            return Ok(loginResponse);
        }

        #endregion Login

        #region Refresh Token
        [HttpGet("refresh-token")]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> RefreshTokenAsync([FromQuery] RefreshTokenRequest refreshTokenRequest)
        {
            var loginResponse = await _refreshTokenService.VerifyRefreshTokenAsync(refreshTokenRequest);
            
            if (loginResponse == null)
            {
                _logger.LogInformation("Invalid refresh token for user: {0}", refreshTokenRequest.RefreshToken);
                return BadRequest("Invalid refresh token");
            }

            var refreshToken = await _refreshTokenService.AddOrUpdateAsync(loginResponse.Id);

            var claims = await GetValidClaims(loginResponse);
            loginResponse.Token = await GenerateToken(claims);
            loginResponse.RefreshToken = refreshToken;

            return Ok(loginResponse);
        }
        #endregion Refresh Token

        #region Delete
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync()
        {
            if(UserId > 0)
            {
                await _userService.DeleteAsync(UserId);
            }
            else
            {
                return BadRequest("You are not logged in user");
            }
            
            return Ok();
        }
        #endregion Delete
    }
}

