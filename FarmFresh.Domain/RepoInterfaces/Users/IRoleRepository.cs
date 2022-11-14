using FarmFresh.Application.Enums;
using FarmFresh.Domain.Entities.Users;

namespace FarmFresh.Domain.RepoInterfaces.Users
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        #region Get
        Task<IList<string>> GetRoleNamesByUserIdAsync(int userId);
        Task<Role> GetByTypeAsync(RoleType roleType);
        #endregion Get
    }
}
