using FarmFresh.Application.Dto.Request.Users;
using FarmFresh.Application.Dto.Response.Users;
using FarmFresh.Application.Enums;

namespace FarmFresh.Application.Interfaces.Services.Users
{
    public interface ILocationService
    {
        #region Get
        Task<List<LocationResponse>> GetAllLocationByUserIdAsync(int userId);
        #endregion Get
        Task<LocationResponse> GetAsync(int userId, LocationType locationType);
        #region Save
        Task UpsertAsync(LocationRequest locationRequest, int userId);
        #endregion Save
    }
}
