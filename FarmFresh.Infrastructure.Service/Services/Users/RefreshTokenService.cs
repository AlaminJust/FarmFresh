using AutoMapper;
using FarmFresh.Application.Dto.Request.Users;
using FarmFresh.Application.Dto.Response.Users;
using FarmFresh.Application.Interfaces.Services.Users;
using FarmFresh.Domain.Entities.Users;
using FarmFresh.Domain.RepoInterfaces.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using System.Security.Cryptography;

namespace FarmFresh.Infrastructure.Service.Services.Users
{
    public class RefreshTokenService : IRefreshTokenService
    {
        #region Properties
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
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
        public async Task<LoginResponse> GetUserByRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
        {
            var refreshToken = await _refreshTokenRepository
                           .GetByCondition(r => r.Token == refreshTokenRequest.RefreshToken)
                           .Include(r => r.User)
                           .SingleOrDefaultAsync();

            if (refreshToken is null || !refreshToken.IsActive)
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
            var refreshToken = new RefreshToken
            {
                UserId = userId,
                RevokedOn = DateTime.UtcNow,
                Token = GenerateRefreshToken(),
                Expires = DateTime.UtcNow.AddDays(7),
                CreatedByIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                CreatedOn = DateTime.UtcNow
            };

            await _refreshTokenRepository.AddAsync(refreshToken);
            await _refreshTokenRepository.SaveChangesAsync();
            return refreshToken.Token;
        }

        #endregion Save

        #region Update

        private async Task<string> UpdateAsync(int userId)
        {
            var refreshToken = await _refreshTokenRepository.GetByCondition(x => x.UserId == userId).FirstOrDefaultAsync();

            refreshToken.RevokedOn = DateTime.UtcNow;
            refreshToken.ReplacedByToken = refreshToken.Token;
            refreshToken.UpdatedOn = DateTime.UtcNow;
            refreshToken.RevokedByIp = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            refreshToken.Expires = DateTime.UtcNow.AddDays(7);
            refreshToken.Token = GenerateRefreshToken();

            await _refreshTokenRepository.UpdateAsync(refreshToken);
            await _refreshTokenRepository.SaveChangesAsync();

            return refreshToken.Token;
        }
        #endregion Update
    }
}
