using FarmFresh.Application.Dto.Request.Users;
using FarmFresh.Application.Dto.Response.Users;

namespace FarmFresh.Application.Interfaces.Services.Users
{
    public interface IRefreshTokenService
    {
        #region Save
        Task<string> AddOrUpdateAsync(int userId);
        #endregion Save

        #region Get
        Task<LoginResponse> VerifyRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
        #endregion Get
    }
}
