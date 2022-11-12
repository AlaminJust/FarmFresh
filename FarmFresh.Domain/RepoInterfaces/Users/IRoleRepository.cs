using FarmFresh.Core.Enums;
using FarmFresh.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
