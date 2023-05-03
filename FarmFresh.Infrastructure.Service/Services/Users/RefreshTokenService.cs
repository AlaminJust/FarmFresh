using AutoMapper;
using FarmFresh.Application.Dto.Request.Users;
using FarmFresh.Application.Dto.Response.Users;
using FarmFresh.Application.Interfaces.Services.Users;
using FarmFresh.Domain.Entities.Users;
using FarmFresh.Domain.RepoInterfaces.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace FarmFresh.Infrastructure.Service.Services.Users
{
    public class RefreshTokenService : IRefreshTokenService
    {
        #region Properties
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private const int RefreshTokenExpiryInDays = 10;
        #endregion Properties

        #region Ctor
        public RefreshTokenService(
                IRefreshTokenRepository refreshTokenRepository,
                IHttpContextAccessor httpContextAccessor,
                IUserService userService,
                IMapper mapper
            )
        {
            _refreshTokenRepository = refreshTokenRepository;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _mapper = mapper;
        }
        #endregion Ctor

        #region Private Method
        public string GenerateRefreshToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        #endregion Private Method

        #region Get
        public async Task<LoginResponse> VerifyRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
        {
            var refreshToken = await _refreshTokenRepository
                           .GetByCondition(r => r.UserId == refreshTokenRequest.UserId)
                           .Include(r => r.User)
                           .SingleOrDefaultAsync();

            if (refreshToken is null || !refreshToken.IsActive)
            {
                return null;
            }

            if(!BC.Verify(refreshTokenRequest.RefreshToken, refreshToken.Token))
            {
                return null;
            }

            return _mapper.Map<LoginResponse>(refreshToken.User);
        }
        #endregion Get

        #region Save

        public async Task<string> AddOrUpdateAsync(int userId)
        {
            var existingToken = await _refreshTokenRepository.GetByCondition(x => x.UserId == userId).FirstOrDefaultAsync();

            if (existingToken is not null)
            {
                return await UpdateAsync(userId);
            }
            else
            {
                return await AddAsync(userId);
            }
        }

        private async Task<string> AddAsync(int userId)
        {
            string actutalToken = GenerateRefreshToken();
            
            var refreshToken = new RefreshToken
            {
                UserId = userId,
                RevokedOn = DateTime.UtcNow,
                Token = BC.HashPassword(actutalToken),
                Expires = DateTime.UtcNow.AddDays(RefreshTokenExpiryInDays),
                CreatedByIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                CreatedOn = DateTime.UtcNow
            };

            await _refreshTokenRepository.AddAsync(refreshToken);
            await _refreshTokenRepository.SaveChangesAsync();
            return actutalToken;
        }

        #endregion Save

        #region Update
        private async Task<string> UpdateAsync(int userId)
        {
            var refreshToken = await _refreshTokenRepository.GetByCondition(x => x.UserId == userId).FirstOrDefaultAsync();
            string actutalToken = GenerateRefreshToken();

            refreshToken.RevokedOn = DateTime.UtcNow;
            refreshToken.ReplacedByToken = refreshToken.Token;
            refreshToken.UpdatedOn = DateTime.UtcNow;
            refreshToken.RevokedByIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            refreshToken.Expires = DateTime.UtcNow.AddDays(RefreshTokenExpiryInDays);
            refreshToken.Token = BC.HashPassword(actutalToken);

            await _refreshTokenRepository.UpdateAsync(refreshToken);
            await _refreshTokenRepository.SaveChangesAsync();

            return actutalToken;
        }
        #endregion Update
    }
}
