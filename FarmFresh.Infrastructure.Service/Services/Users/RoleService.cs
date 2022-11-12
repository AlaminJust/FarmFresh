using FarmFresh.Application.Interfaces.Services.Users;
using FarmFresh.Domain.RepoInterfaces.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmFresh.Infrastructure.Service.Services.Users
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(
                IRoleRepository roleRepository
            )
        {
            _roleRepository = roleRepository;
        }
        public async Task<IList<string>> GetRoleNamesByUserIdAsync(int userId)
        {
            return await _roleRepository.GetRoleNamesByUserIdAsync(userId);
        }
    }
}
