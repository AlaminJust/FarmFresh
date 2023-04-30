using FarmFresh.Application.Interfaces.Services.Users;
using FarmFresh.Domain.RepoInterfaces.Users;

namespace FarmFresh.Infrastructure.Service.Services.Users
{
    public class RoleService : IRoleService
    {
        #region Properties
        private readonly IRoleRepository _roleRepository;
        #endregion Properties

        #region Constructor
        public RoleService(
                IRoleRepository roleRepository
            )
        {
            _roleRepository = roleRepository;
        }
        #endregion Constructor

        #region Get
        public async Task<IList<string>> GetRoleNamesByUserIdAsync(int userId)
        {
            return await _roleRepository.GetRoleNamesByUserIdAsync(userId);
        }
        #endregion Get
    }
}
