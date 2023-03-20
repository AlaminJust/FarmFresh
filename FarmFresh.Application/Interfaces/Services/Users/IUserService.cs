using FarmFresh.Application.Dto.Request.Users;
using FarmFresh.Application.Dto.Response.Users;
using FarmFresh.Domain.Dto.Request.Users;

namespace FarmFresh.Application.Interfaces.Services.Users
{
    public interface IUserService
    {
        #region Get
        Task<LoginResponse> LoginAsync(LoginRequest loginRequest);
        Task<UserResponse> FindByIdAsync(int userId);
        #endregion Get

        #region Save
        Task AddAsync(UserRequest userRequest);
        #endregion Save

        #region Update
        Task UpdateAsync(int userId, UserAddressRequest userRequest);
        #endregion Update
    }
}
