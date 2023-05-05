using FarmFresh.Application.Dto.Request.Users;

namespace FarmFresh.Application.Interfaces.Services.Users
{
    public interface ILocationService
    {
        #region Save
        Task UpsertAsync(LocationRequest locationRequest, int userId);
        #endregion Save
    }
}
